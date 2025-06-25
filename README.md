

# Copeland Assignment

## Inputs & Outputs:
  - Sensor data is placed in the `data` folder.
    - Any file directly in this folder is regarded as data and pulled in for processing.
  - The `data` folder has an `output` folder, where the app outputs are placed.

## General Overview
  - `ApplicationServices` Folder
    - Includes application level services for data transport and the initial processing service.
    - `DataProcessor` Is the main entry point of the application.  It loads the data, calls the processor, and then saves it to disk.
      - Accepts abstract services so data could theoretically come from other sources, and be sent to other destinations.
  - `DataNormalization` Folder
    - Contains the business logic to solve our problem.
    - `DataNormalizer` is the main entry for processing data.
      - This service uses the `IDataNormalizerService` implementations (one for `Foo1`, and one for `Foo2`) to identify their respective data, and provide processing for it.
      - The `DataNormalizer` will collect the data from the individual services, combine it, and return it.

## Unit Tests
I picked the most conservative approach to unit testing.  There are different schools of thought regarding unit tests.  If the approach I took was too heavy, then it's easy to lighten up.

## Final Comments
I took the task as straight forward as the request.  I could have went deeper, but decided not to do too much more than was requested.  On the other hand, I'd expect datasets to be enormous, processing a single file in a single pass is probably not realistic for the real world.

I realize I could have used `async` functions (for file operations).  Again, I went with the most straight forward approach, but would have had no problems doing so if needed.

