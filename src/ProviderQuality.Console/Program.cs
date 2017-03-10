using System.Collections.Generic;

namespace ProviderQuality.Console
{
    public class Program
    {
        public IList<Award> Awards
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Updating award metrics...!");

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Gov Quality Plus", SellIn = 10, Quality = 20},
                    new Award {Name = "Blue First", SellIn = 2, Quality = 0},
                    new Award {Name = "ACME Partner Facility", SellIn = 5, Quality = 7},
                    new Award {Name = "Blue Distinction Plus", SellIn = 0, Quality = 80},
                    new Award {Name = "Blue Compare", SellIn = 15, Quality = 20},
                    new Award {Name = "Top Connected Providres", SellIn = 3, Quality = 6},
                    new Award {Name = "Blue Star", SellIn = 10,Quality = 50}
                }

            };

            app.UpdateQuality();

           System.Console.ReadKey();
           

        }//main


        public void UpdateQuality()
        {
            for (var i = 0; i < Awards.Count; i++)
            {

                //  For easier maintenance, each award type processing was separated out 
                //   to reduce impact of individual award category rules on other award types


                if (Awards[i].Name != "Blue Distinction Plus") //No processing for Blue Distinction Plus
                {
                   
                    if (Awards[i].Name== "Blue Star")  //Process Blue Star
                    {
                        if (Awards[i].SellIn < 0)
                        { Awards[i].Quality -= 4; } //after expiration double its depreciation rate
                        else
                        { Awards[i].Quality -= 2; } //if not expired, 2x normal depreciation rate


                    }

                    
                    else if (Awards[i].Name == "Blue Compare") //Process Blue Compare
                    {
                        if (Awards[i].SellIn < 0) 
                        { Awards[i].Quality = 0; } //if expired quality is 0
                      
                        else if (Awards[i].SellIn > 5 && Awards[i].SellIn <= 10)
                        { Awards[i].Quality += 2; } // 6-10 days, double appreciation
                      
                        else if (Awards[i].SellIn <= 5)
                        { Awards[i].Quality += 3; } //5 days or less, triple appreciation
                      
                        else
                        { Awards[i].Quality += 1; }   // otherwise, single appreciation

                    }

                    else if (Awards[i].Name == "Blue First") //Process Blue First
                    {
                        Awards[i].Quality += 1;
                        //In the original code, Blue First appreciates 2x rate after expiration
                        // But I cannot find this in the requirements
                        /*   If this is the requirement  then the code shoud be
                            If(Awards[i].SellIn < 0)
                            { 
                               Awards[i].Quality += 2; 
                            }
                             else
                            { 
                               Awards[i].Quality += 1; 
                            }   
                        */
                    }

                    else //other types of award
                    {
                        if (Awards[i].SellIn < 0)
                        { Awards[i].Quality -= 2; } //if expired, double depreciation                       
                        else
                        { Awards[i].Quality -= 1; } // if not expired, single depreciation
                    }
    

                    if (Awards[i].Quality > 50)
                    { Awards[i].Quality = 50; }  //quality is never above 50 except blue distinction plus

                    Awards[i].SellIn--;


                } //end if not blue distinction plus
               



          
               /* if (Awards[i].Name != "Blue First" && Awards[i].Name != "Blue Compare")
                {
                    if (Awards[i].Quality > 0)
                    {
                        if (Awards[i].Name != "Blue Distinction Plus")
                        {
                            Awards[i].Quality = Awards[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Awards[i].Quality < 50)
                    {
                        Awards[i].Quality = Awards[i].Quality + 1;

                        if (Awards[i].Name == "Blue Compare")
                        {
                            if (Awards[i].SellIn < 11)
                            {
                                if (Awards[i].Quality < 50)
                                {
                                    Awards[i].Quality = Awards[i].Quality + 1;
                                }
                            }

                            if (Awards[i].SellIn < 6)
                            {
                                if (Awards[i].Quality < 50)
                                {
                                    Awards[i].Quality = Awards[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Awards[i].Name != "Blue Distinction Plus")
                {
                    Awards[i].SellIn = Awards[i].SellIn - 1;
                }

                if (Awards[i].SellIn < 0)
                {
                    if (Awards[i].Name != "Blue First")
                    {
                        if (Awards[i].Name != "Blue Compare")
                        {
                            if (Awards[i].Quality > 0)
                            {
                                if (Awards[i].Name != "Blue Distinction Plus")
                                {
                                    Awards[i].Quality = Awards[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Awards[i].Quality = Awards[i].Quality - Awards[i].Quality;
                        }
                    }
                    else
                    {
                        if (Awards[i].Quality < 50)
                        {
                            Awards[i].Quality = Awards[i].Quality + 1;
                        }
                    }
                    
                }*/

                System.Console.WriteLine(Awards[i].Name + ": Q= " + Awards[i].Quality.ToString() + ", Exp= " + Awards[i].SellIn.ToString());


            } //for loop

        }//updatequality
        

    }//class

}//namespace
