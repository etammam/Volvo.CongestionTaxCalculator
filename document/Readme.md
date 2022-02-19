i have cloned my college code and start testing it before creating the restful APIs
but unfortunately i found couple of logical errors by my unit tests as following

- `IsTollFreeDate(DateTime date)`  
the function that responsible for validate the date against bunch of cases   
weekend **(Saturday, Sunday)**,  
unsupported year (out of **2013**)  
holiday and i have concern here because it's hard coded and support only some dates not all in the year
and i will going to fix it in my refactor.  
no implementation for ignored tax in month, so i need to implement this  
    - **Status**: **Working**   
    - **Limitation**: we are not able to extend the year supporting unless hard code the fees time map,
holidays, ignored months, etc.

- `IsTollFreeVehicle(Vechivle vehicle)`  
the function that responsable for validating the vehicle type against the 
tax exempt vehicles
when tested with Motorbike as an instance of vehicle it's returns false
because the defined Enum dose not contains the definition of motorbike but instead 
it's contains a motorcycle definition 
and to make it pass, i changed the class name from the motorbike to motorcycle
as same as in business and the Enum definition, and ofcourse the `GetVehicleType()` to return
name of motorcycle instead of motorbike  
    - **Status**: **Working**   
    - **Limitation**: Mapping between interface implementation and Enum, 
"Over engineering" because we need to create the same map again in endpoint


- `GetTax(Vehicle vehicle, DateTime[] dates)`
the function that responsable for returning the fee of tax according inputs while testing, i found a bug 
in calculating minutes between dates, so it's ignoring the condition of calculating value between more than 60 minuts
so it's always return the max value of tax according the dates.  
*my solution* that is refactor the write function for substracting the time.
and now it's working,  
    - **Status**: **Working**    
    - **limitation**: it's working with range of dates only, no ability to working 
with recorded history

> after complete the first task, found a lot of limitations and performance issues, i decided to refactor the solution to make it run with dynamic tax rulers and ignorance
first of all you can find

### Refactor
in order to build a new application with restful endpoint, i build a new application on clean architecture and live database, with two deferent layers of unit tests 
to keep eyes on.

#### Assets
1. [Application Architecture](https://github.com/etammam/Volvo.CongestionTaxCalculator/blob/master/document/Volvo.CongestionTaxCalculator.Architecture.drawio)
2. [User Journey](https://github.com/etammam/Volvo.CongestionTaxCalculator/blob/master/document/Volvo.CongestionTaxCalculator.Journey.drawio)
3. External Dependencies  
   - [Guard Clause](https://github.com/ardalis/GuardClauses)
   - [Nager Date](https://github.com/nager/Nager.Date)

#### Modules
application build with various modules, City management, Rules Or Rates Management, Tax Histories, Tax Payment  
unforuntly for keep on time i was unable to implement all of this modules
first of all 
1. City Management
    you are able to create, edit and list cities which will be supported on system 
    so you are able to log taxes in difference cities eg. Stockholm.
    also you have ability to set the ignores monthes, and add a exceptional number of days tax will be free in 
    before holiday days.
2. Rule Management
   you are able to create, edit, list the city rules to set (start, end, fee) of each rule
   new you can ignore definition of holidays because it will be conceded automatic using **Nager Date**
   also, I'm using the new dotnet 6 capability of time only
3. Tax Management
   you can add tax to vehicle tax in vehicle tax history to calculate this next or lising this
   by calculating the vehicle recorded taxes grouped by date


#### Endpoints
1. City Management
   1. Get List Of System Cities  GET:(https://localhost:7239/api/city)
      ```json
        {
          "statusCode": 200,
          "success": true,
          "message": "your operation completed successfully",
          "model": [
            {
              "id": "9d315725-c8a0-45e1-9a55-fb480a477ab9",
              "name": "Gothenburg",
              "code": 1,
              "ignoredMonths": [
                "July"
              ],
              "ignoredDaysBeforeHoliday": 1
            },
            {
              "id": "791e46a2-52ea-46f3-bfcf-25d39320911f",
              "name": "Stockholm",
              "code": 2,
              "ignoredMonths": [],
              "ignoredDaysBeforeHoliday": 0
            }
          ],
          "errors": null
        }
   
   2. Create New City POST:(https://localhost:7239/api/city)
      
      Request
      ```json
      {
         "name": "string", //the city name eg. Lund
         "code": 0, //a city code used as an identifier in system eg. 3
         "ignoreMonths": [ //list of monthes that should be ignore taxs in, you can ignore this.
         "string" // eg. March
         ],
         "ignoredDaysBeforeHoliday": 0 //the count of exceptional days to ignore before holiday
      }
      ```
      
      and return
      
        ```json
        {
            "statusCode": 201,
            "success": true,
            "message": "your operation completed successfully",
            "model": {
            "id": "1c69c5d7-b117-40c7-ab8e-25caa8e3e6c4",
            "name": "Lund",
            "code": 6,
            "ignoredMonths": [],
            "ignoredDaysBeforeHoliday": 0
            },
            "errors": null
        }
        ```
   
   3.Update City: same as Create but it's requires city id in route POST:(https://localhost:7239/api/city/1c69c5d7-b117-40c7-ab8e-25caa8e3e6c4)
   
2. Rule Management
   
   1. Create Rate: it's requires city id in request route POST:(https://localhost:7239/api/rate/1c69c5d7-b117-40c7-ab8e-25caa8e3e6c4)
   
      Request it's accept a list of rate 
   
      ```json
      {
        "rates": [
          {
            "start": "6:12:42",
            "end": "6:32:42",
            "rate": 9
          },
          {
            "start": "6:33:00",
            "end": "7:00:00",
            "rate": 13
          }
        ]
      }
      ```
   
      and it's return
      ```json
       {
            "statusCode": 201,
            "success": true,
            "message": "your operation completed successfully",
            "model": {
            "cityId": "1c69c5d7-b117-40c7-ab8e-25caa8e3e6c4",
            "cityName": "Lund",
            "cityCode": 6,
            "rates": [
                  {
                     "id": "1b11dd54-406a-4647-9666-75f794bd8404",
                     "start": "00:11:42.0000000",
                     "end": "00:11:42.0000000",
                     "rate": 14
                  }
            	]
            },
            "errors": null
       }
      ```
   
   2. Get it's requires city id in route GET:(https://localhost:7239/api/rate/1c69c5d7-b117-40c7-ab8e-25caa8e3e6c4)
   
      ```json
      {
        "statusCode": 200,
        "success": true,
        "message": "your operation completed successfully",
        "model": [
          {
            "id": "1b11dd54-406a-4647-9666-75f794bd8404",
            "start": "00:11:42.0000000",
            "end": "00:11:42.0000000",
            "rateValue": 14
          }
        ],
        "errors": null
      }
      ```
   
    3. Update rates it's replace all of city rates and it's same as create PUT:(https://localhost:7239/api/rate/1c69c5d7-b117-40c7-ab8e-25caa8e3e6c4)
   
3. Tax Management

   1. Create tax: it's record a vehicle tax in city and it's requires city id in route
      POST: (https://localhost:7239/api/taxs/vehicle/TRW-12254)

      ```json
      {
        "vehicleId": "TRW-12254",
        "taxCost": 8,
        "issued": "2013-01-08T06:14:51.926Z",
        "vehicleType": 7
      }
      ```

      and it's return 

      ```json
      {
        "statusCode": 201,
        "success": true,
        "message": "your operation completed successfully",
        "model": {
          "id": "e658e07a-fc97-4c61-9f35-ede977a4f1e0",
          "cityId": "9d315725-c8a0-45e1-9a55-fb480a477ab9",
          "cityName": "Gothenburg",
          "cityCode": 1,
          "vehicleId": "TRW-12254",
          "taxCost": 8,
          "issued": "2013-01-08T06:14:51.926Z",
          "vehicleType": "Car"
        },
        "errors": null
      }
      ```

   2.  get the recorded tax for vehicle GET:(https://localhost:7239/api/taxs/TRW-12254)
      the returned value present the vehicle statement report and it's grouped by date as issue

      ```json
      {
        "statusCode": 200,
        "success": true,
        "message": "your operation completed successfully",
        "model": {
          "vehicleId": "TRW-12254",
          "histories": [
            {
              "cityId": "9d315725-c8a0-45e1-9a55-fb480a477ab9",
              "cityName": "Gothenburg",
              "issue": "2013-01-08T00:00:00",
              "amount": 8,
              "tax": [
                {
                  "id": "e658e07a-fc97-4c61-9f35-ede977a4f1e0",
                  "captureAt": "06:14:00.0000000",
                  "cost": 8,
                  "vehicleType": "Car"
                }
              ]
            }
          ]
        },
        "errors": null
      }
      ```

   3. get history of vehicle taxes GET:(https://localhost:7239/api/taxs/vehicle/TRW-12254)

      ```json
      {
        "statusCode": 200,
        "success": true,
        "message": "your operation completed successfully",
        "model": [
          {
            "id": "e658e07a-fc97-4c61-9f35-ede977a4f1e0",
            "cityName": "Gothenburg",
            "taxCost": 8,
            "issue": "2013-01-08T06:14:51.926",
            "vehicleType": "Car"
          }
        ],
        "errors": null
      }
      ```

   4. get history of city recorded taxes GET:(https://localhost:7239/api/taxs/city/9d315725-c8a0-45e1-9a55-fb480a477ab9)

      ```json
      {
        "statusCode": 200,
        "success": true,
        "message": "your operation completed successfully",
        "model": [
          {
            "id": "50badcb2-1390-4c00-98ca-71b928390dc8",
            "cityName": "Gothenburg",
            "taxCost": 8,
            "issue": "2013-01-18T08:46:37.889",
            "vehicleType": "Emergency"
          },
          {
            "id": "a8030e59-6a37-4d59-8dc4-4ea5d398f53e",
            "cityName": "Gothenburg",
            "taxCost": 8,
            "issue": "2013-01-22T08:46:37.889",
            "vehicleType": "Emergency"
          },
          {
            "id": "52c41f90-10b6-4068-bf6f-b6adbb0604b7",
            "cityName": "Gothenburg",
            "taxCost": 13,
            "issue": "2013-01-18T08:12:37.889",
            "vehicleType": "Emergency"
          },
          {
            "id": "e658e07a-fc97-4c61-9f35-ede977a4f1e0",
            "cityName": "Gothenburg",
            "taxCost": 8,
            "issue": "2013-01-08T06:14:51.926",
            "vehicleType": "Car"
          }
        ],
        "errors": null
      }
      ```



#### Kickoff

you can clone the source code and run it, then browse the swagger ui.
or you can build the docker image using dockerfile in cloned source
you can browse code into your ide and run unit tests 

