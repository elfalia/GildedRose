using System;
using NUnit.Framework;
using System.Collections.Generic;
using ApprovalTests.Reporters;

namespace GildedRose
{
	[TestFixture()]

    [UseReporter(typeof(NUnitReporter))]
    public class GildedRoseTest
	{
        /// <summary>
        /// Quality of an item is never negative
        /// </summary>
		[Test()]
		public void Test2() {
			IList<Item> items = new List<Item> { new Item{Name = "My user", SellIn = 0, Quality = 0} };
			GildedRose app = new GildedRose(items);
			app.UpdateQuality();
            Assert.GreaterOrEqual(items[0].Quality, 0);

        }


        /// <summary>
        /// Once the sell by date has passed, Quality degrades twice as fast
        /// </summary>
        [TestCase(2,0,2)] 
        [TestCase(2,1,1)] 
        public void Test1(int a, int b,int c)
        {
            IList<Item> items = new List<Item> { new Item { Name = "My user", SellIn = a, Quality = 6 } };
            GildedRose app = new GildedRose(items);
            var fQuality = items[0].Quality;
            app.UpdateQuality();
            var deltaQ = items[0].Quality - fQuality;

            IList<Item> items1 = new List<Item> { new Item { Name = "My user", SellIn = b, Quality = 6 } };
            GildedRose app2 = new GildedRose(items1);
            var fQuality2 = items1[0].Quality;
            app2.UpdateQuality();
            var deltaQ2 = items1[0].Quality - fQuality2;

            Assert.AreEqual(deltaQ*c, deltaQ2);
         
        }

        /// <summary>
        /// "Aged Brie" actually increases in Quality the older it gets
        /// </summary>
        [Test()]
        public void Test3()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 7, Quality = 6 } };
            GildedRose app = new GildedRose(items);
            var fQuality = items[0].Quality;
            app.UpdateQuality();
            Assert.Greater(items[0].Quality, fQuality);

        }


        /// <summary>
        /// The Quality of an item is never more than 50
        /// </summary>
        [Test()]
        public void Test4()
        {
            // Quality can increase only in special ways (Name = "Aged Brie" "Backstage passes to a TAFKAL80ETC concert")
            // Name ="Sulfuras" quality=80 (more than 50) 
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 11, Quality = 50 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Greater(51, items[0].Quality);

        }



        /// <summary>
        /// "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        /// </summary>
        [Test()]
        public void Test5()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 11, Quality = 6 } };
            GildedRose app = new GildedRose(items);
            var fSellIn = items[0].SellIn;
            var fQuality = items[0].Quality;
            app.UpdateQuality();
            Assert.AreEqual(items[0].SellIn, fSellIn);
            Assert.GreaterOrEqual(items[0].Quality, fQuality);
        }



        /// <summary>
        /// "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches;
        ///  Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
        ///  Quality drops to 0 after the concert
        /// </summary>
        [TestCase(12,1)]
        [TestCase(8, 2)]
        [TestCase(3, 3)]
        public void Test6(int a, int b)
        {
            IList<Item> items1 = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = a, Quality = 6 } };
            GildedRose app1 = new GildedRose(items1);
            var fQuality1 = items1[0].Quality;
            app1.UpdateQuality();
            var deltaQ1 = items1[0].Quality - fQuality1;
            Assert.AreEqual(b, deltaQ1);


            //IList<Item> items2 = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = a, Quality = 6 } };
            //GildedRose app2 = new GildedRose(items2);
            //var fQuality2 = items2[0].Quality;
            //app2.UpdateQuality();
            //var deltaQ2 = items2[0].Quality - fQuality2;
            //Assert.AreEqual(b, deltaQ2);

            //IList<Item> items3 = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 6 } };
            //GildedRose app3 = new GildedRose(items3);
            //app3.UpdateQuality();
            //Assert.AreEqual(0, items3[0].Quality);

        }

       [Test()]
        public void Test6_1()
        {

            IList<Item> items3 = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 6 } };
            GildedRose app3 = new GildedRose(items3);
            app3.UpdateQuality();
            Assert.AreEqual(0, items3[0].Quality);

        }




        /// <summary>
        /// No implemention
        /// "Conjured" items degrade in Quality twice as fast as normal items 
        /// </summary>
        [Test()]
        public void Test7()
        {
            // My user is normal Item
            IList<Item> items = new List<Item> { new Item { Name = "My user", SellIn = 3, Quality = 6 } };
            GildedRose app = new GildedRose(items);
            var fQuality = items[0].Quality;
            app.UpdateQuality();
            var deltaQ = items[0].Quality - fQuality;

            IList<Item> items2 = new List<Item> { new Item { Name = "Conjured", SellIn = 3, Quality = 6 } };
            GildedRose app2 = new GildedRose(items2);
            var fQuality2 = items2[0].Quality;
            app2.UpdateQuality();
            var deltaQ2 = items2[0].Quality - fQuality2;
            Assert.AreEqual(deltaQ * 2, deltaQ2);
        }


        /// <summary>
        /// "Sulfuras" is a
        /// legendary item and as such its Quality is 80 and it never alters
        /// </summary>
        [TestCase(34)]
        [TestCase(80)]
        public void Test8(int a)
        {
            IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 11, Quality = a } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(items[0].Quality, 80);
        }

    }
}

