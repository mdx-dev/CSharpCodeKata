using System;
using System.Collections.Generic;

using ProviderQuality.Console.Award;

namespace ProviderQuality.Console
{
    /* Since this is a coding test, I figured I would give some context to my thoughts since it's not
     * always clear why a certain design decision was made.
     * 
     * OPINION ALERT!  :)
     * 
     * This problem was kind of screaming 'default update algorithm in the parent class, override for 
     * special cases in the child class', but I don't necessarily like that approach unless there's a
     * REALLY strong argument for it.  In a small program like this it isn't really problematic, but in
     * the long term it creates really tight coupling, you lose locality of reference, and making
     * structural changes to the design tends to be harder because you can't do it piecemeal.
     * 
     * For that reason, I chose instead to pull out the idea of an algorithm into it's own type and let
     * the awards wire themselves up to the appropriate algorithm (with the appropriate inputs).  
     * 
     * 1. It's more testable
     * 2. It switches the mental model away from 'award' to 'update algorithm' where it belongs (imo).
     * 3. It allows for more consistency.  What I mean by this is that now I can ALWAYS look into an
     *    award constructor and see how it's being updated.  In my opinion that consistency is more
     *    important for future maintenance than the "code reuse" you would get going with the canonical
     *    approach.
     * 
     * Speaking of tests, some of the tests overlap between the algorithm tests and the award tests.  I
     * think this is ok, the algorithm tests are making sure the behavior is correct, the award tests are
     * making sure the algorithm being used in the award is correct.
     * 
     * I chose not to test the award names.  The names aren't specified in the documentation and I don't 
     * necessarily like the idea of a name change requiring tests being updated.  I honestly don't feel
     * testing that would add anything other than more work over the long term.  Names don't really change
     * the correct functioning of the software, and in production software you're probably getting the name
     * dynamically anyway (internationalization).
     * 
     * I also created an 'AwardSet' class, which really wasn't all that useful for this program.  I tried
     * giving it a few extra methods to demonstrate what I think it would be useful about using a class
     * like this instead of a raw collection of awards.
     * 
     * The algorithms don't update the expiration.  I didn't feel like it was an appropriate thing for them
     * to be doing unless it was explicit (Algorithm#IncrementExpirationAndRun(), for example).  I think
     * this decision could be a bit controversial as it creates a bit more complexity in the correct updating
     * of the awards, but I believe long term it would save more time than it cost.
     */
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Updating award metrics...!");

            var set = new AwardSet(new IAward[]
            {
                new GovQualityPlusAward(20,TimeSpan.FromDays(10)),
                new BlueFirstAward(0,TimeSpan.FromDays(2)),
                new AcmePartnerFacilityAward(7,TimeSpan.FromDays(5)),
                new BlueDistinctionPlusAward(TimeSpan.FromDays(0)),
                new BlueCompareAward(20,TimeSpan.FromDays(15)),
                new TopConnectedProvidersAward(3,TimeSpan.FromDays(6)),
                new BlueFirstAward(30,TimeSpan.FromDays(3)),
            });
            set.UpdateAwards();

            System.Console.ReadKey();
        }
    }
}
