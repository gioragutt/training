using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ModuloZero.BaseGameClasses
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public string Asset { get; set; }
    }

    /// <summary>
    /// Loads item list from XML document
    /// </summary>
    public static class ItemLoader
    {
        /// <summary>
        /// Returns a list of items
        /// </summary>
        /// <param name="xmlPath">name\path of the xml file contatining the items</param>
        /// <returns>List of type Item</returns>
        public static List<Item> LoadItems(string xmlPath)
        {
            try
            {
                var xml = XDocument.Load("itemsdoc.xml");

                if (xml.Root == null)
                    throw new Exception();
                var items = xml.Root.Elements("item").
                                Select(e => new Item()
                                {
                                    ID = int.Parse(e.Element("id").Value),
                                    Name = e.Element("name").Value,
                                    Cost = int.Parse(e.Element("cost").Value),
                                    Description = e.Element("description").Value,
                                    Asset = e.Element("asset").Value
                                }).ToList();

                return items;
            }
            catch
            {
                return new List<Item>();
            }
        }
    }
}
