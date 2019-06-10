# Sapphire Digital (.NET) Code Test

# Description
Hi, and welcome to the team! As you know, we provide tools for searching for doctors and hospitals (i.e. "providers") based on various criteria. One type of search that we provide is to find providers that are highly rated by quality.

To calculate quality, one measure we use is a score based on professional award recognitions. Each award is factored in slightly differently.

- All awards have an ExpiresIn value which denotes the number of days until the award expires.

- All awards have a Quality value which denotes how valuable the award is in our overall calculation.

- At the end of each day our system recalculates both values for every award based on business rules.

Pretty basic. But here is where it gets interesting...

  - Once the expiration date has passed, quality score degrades twice as fast

  - The quality of an award is never negative.
  
  - "Blue First" awards actually increase in quality the older they get

  - The quality of an award is never more than 50

  - "Blue Distinction Plus", being a highly sought distinction, never decreases in quality

  - "Blue Compare", similar to "Blue First", increases in quality as the expiration date approaches; Quality increases by 2 when there are 10 days or less left, and by 3 where there are 5 days or less left, but quality value drops to 0 after the expiration date.

  - Just for clarification, an award can never have its quality increase above 50, however "Blue Distinction Plus", being highly sought, its quality is 80 and it never alters.

We have recently gotten a request from our clients to include an additional award in our quality calculations called "Blue Star". This requires an update to our system.

# Your Assignment

Here is the business story:

- In order to distinguish between providers of high quality, as a consumer, I want to see "Blue Star" awarded providers near the top of the results when the award is initially granted, but its' impact should be smaller the longer it has been from the grant date.

- Acceptance Criteria
  - "Blue Star" awards should lose quality value twice as fast as normal awards.

If you are familiar with Git (preferred) please fork the code and submit a link to your fork with your changes when you have completed the assignment.  If you are not familiar with Git you may download the source as a zip file and later submit the modified archive.  In all cases you should be able to use a free version of Visual Studio (on Windows) to make the changes.

The existing code is "legacy", and, ugh, it's ugly. Feel free to make any changes to the code as long as everything still works correctly.

## Installation Hints
Once loaded in Visual Studio any dependent nuget packages should restore automatically.  You can also run the Build.bat file from the command line to restore packages and build the code.  At the end of the exercise we should have ample confidence in adding the "Blue Star" award to our configuration knowing it will be propery curated when the existing application runs.
