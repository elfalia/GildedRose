using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        private IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        //public void UpdateQuality()
        //{
        //    for (var i = 0; i < Items.Count; i++)
        //    {
        //        if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
        //        {
        //            if (Items[i].Quality > 0)
        //            {
        //                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
        //                {
        //                    Items[i].Quality = Items[i].Quality - 1;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (Items[i].Quality < 50)
        //            {
        //                Items[i].Quality = Items[i].Quality + 1;

        //                if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
        //                {
        //                    if (Items[i].SellIn < 11)
        //                    {
        //                        if (Items[i].Quality < 50)
        //                        {
        //                            Items[i].Quality = Items[i].Quality + 1;
        //                        }
        //                    }

        //                    if (Items[i].SellIn < 6)
        //                    {
        //                        if (Items[i].Quality < 50)
        //                        {
        //                            Items[i].Quality = Items[i].Quality + 1;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
        //        {
        //            Items[i].SellIn = Items[i].SellIn - 1;
        //        }

        //        if (Items[i].SellIn < 0)
        //        {
        //            if (Items[i].Name != "Aged Brie")
        //            {
        //                if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
        //                {
        //                    if (Items[i].Quality > 0)
        //                    {
        //                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
        //                        {
        //                            Items[i].Quality = Items[i].Quality - 1;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    Items[i].Quality = Items[i].Quality - Items[i].Quality;
        //                }
        //            }
        //            else
        //            {
        //                if (Items[i].Quality < 50)
        //                {
        //                    Items[i].Quality = Items[i].Quality + 1;
        //                }
        //            }
        //        }
        //    }
        //}



/// <summary>
/// I don't know rules for refactoring =) I rewrote code like I felt it'll be better 
///
/// </summary>
        public void UpdateQuality()
{
    for (int n = 0; n < Items.Count; n++)
    {
        var item = Items[n];
        switch (Items[0].Name)
        {
            case "Aged Brie":
            {
                if (item.Quality < 50)
                    item.Quality = item.Quality + 1;

                item.SellIn = item.SellIn - 1;
                break;
            }
            case "Backstage passes to a TAFKAL80ETC concert":
            {
                if (item.SellIn <= 0)
                {
                    item.Quality = 0;
                    item.SellIn = item.SellIn - 1;
                    break;
                }

                if (item.Quality < 50)
                    item.Quality = item.Quality + 1;

                if (item.SellIn < 11)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }

                if (item.SellIn < 6)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }

                item.SellIn = item.SellIn - 1;
                break;
            }

            case "Sulfuras, Hand of Ragnaros":
            {
                item.Quality = 80;
                break;
            }

            case "Conjured":
            {
                if (item.Quality <= 1) break;
                item.Quality = item.Quality - 2;

                if (item.SellIn <= 0)
                {
                    if (item.Quality <= 1) break;
                    item.Quality = item.Quality - 2;
                }
                item.SellIn = item.SellIn - 1;
                break;
            }

            //usual item
            default:
            {
                if (item.Quality <= 0) break;
                item.Quality = item.Quality - 1;

                if (item.SellIn <= 0)
                {
                    if (item.Quality <= 0) break;
                    item.Quality = item.Quality - 1;
                }
                item.SellIn = item.SellIn - 1;
                break;
            }
        }
    }
}
    }

    public class Item
	{
		public string Name { get; set; }
		
		public int SellIn { get; set; }
		
		public int Quality { get; set; }
	}
	
}
