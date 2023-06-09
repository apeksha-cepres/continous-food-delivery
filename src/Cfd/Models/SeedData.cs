﻿using System;
using System.Linq;
using Cfd.Models;
using System.Collections.Specialized;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.SqlServer;




namespace Cfd.Models
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new CfdContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CfdContext>>()))
            {
                // Look for any MenuItems.
                if (context.MenuItems.Any())
                {
                    return;   // DB has been seeded
                }

                string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string imagePath = Path.Join(exeDir, "Database", "images");


                List<dynamic> menuListSeed = new List<dynamic>();

                ListDictionary listWater = new ListDictionary();
                listWater.Add("description", "Fresh from the tap");
                listWater.Add("imageId", "water");
                listWater.Add("id", 0);
                listWater.Add("price", 1.99);
                listWater.Add("name", "Water");

                ListDictionary listWrap = new ListDictionary();
                listWrap.Add("description", "Chicken Wrap - Sandwich");
                listWrap.Add("imageId", "wrap");
                listWrap.Add("id", 1);
                listWrap.Add("price", 14.99);
                listWrap.Add("name", "Chicken Wrap");

                ListDictionary listStew = new ListDictionary();
                listStew.Add("description", "A slow cooked stew");
                listStew.Add("imageId", "stew");
                listStew.Add("id", 2);
                listStew.Add("price", 12.99);
                listStew.Add("name", "Stew");

                ListDictionary listSoup = new ListDictionary();
                listSoup.Add("description", "It looks good in the menu picture");
                listSoup.Add("imageId", "soup");
                listSoup.Add("id", 3);
                listSoup.Add("price", 4.99);
                listSoup.Add("name", "Tomato Soup");

                ListDictionary listSalad = new ListDictionary();
                listSalad.Add("description", "A green salad");
                listSalad.Add("imageId", "salad");
                listSalad.Add("id", 4);
                listSalad.Add("price", 4.99);
                listSalad.Add("name", "Salad");

                menuListSeed.Add(listWater);
                menuListSeed.Add(listWrap);
                menuListSeed.Add(listStew);
                menuListSeed.Add(listSoup);
                menuListSeed.Add(listSalad);

                foreach (var menuItem in menuListSeed)
                {
                    MenuItem mi = new MenuItem();
                    Image img = new Image();

                    string fileName = Path.Combine(imagePath, menuItem["imageId"] + ".jpg");

                    mi.Image = img.Add(fileName);
                    mi.Description = menuItem["description"];
                    mi.Price = menuItem["price"];
                    mi.Name = menuItem["name"];
                    context.Images.Add(mi.Image);
                    mi.ImageId = mi.Image.Id;
                    context.MenuItems.Add(mi);
                    context.SaveChanges();
                }
            }
        }

    }
}