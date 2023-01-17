using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CinemaApp.Models
{
    public class Food
    {
        public string foodID { get; set; }
        public string foodName { get; set; }
        public string detail { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

        static public ObservableCollection<Food> InitializeFood()
        {
            return new ObservableCollection<Food>
        {
            new Food
            {
                foodName = "SINGLE BT21 COMBO",
                image = "https://www.cgv.vn/media/concession/web/62fe077bd978b_1660815228.png",
                detail = "1 ly BT21 Back To School 32Oz + 1 nước siêu lớn + 1 bắp 44Oz (tùy chọn vị)",
                price = 309000,
                quantity = 0,
                foodID = "1"
            },
            new Food
            {
                foodName = "POWER COMBO",
                image = "https://www.cgv.vn/media/concession/web/6364866d90d3f_1667532398.png",
                detail = "1 ly Avatar + 1 nước siêu lớn + 1 bắp ngọt lớn",
                price = 239000,
                quantity = 0,
                foodID = "2"
            },
            new Food
            {
                foodName = "KITTEN DOUBLE COMBO",
                image = "https://www.cgv.vn/media/concession/web/63ac180f6f35b_1672222735.png",
                detail = "2 ly Kitten Brown + 2 nước siêu lớn + 1 bắp ngọt lớn",
                price = 499000,
                quantity = 0,
                foodID = "3"
            },
            new Food
            {
                foodName = "KITTEN SINGLE COMBO",
                image = "https://www.cgv.vn/media/concession/web/63ac16ed6b903_1672222445.png",
                detail = "1 ly Kitten Brown + 1 nước siêu lớn + 1 bắp ngọt lớn",
                price = 279000,
                quantity = 0,
                foodID = "4"
            },
            new Food
            {
                foodName = "S MEOW COMBO",
                image = "https://www.cgv.vn/media/concession/web/63ad1756dc183_1672288087.png",
                detail = "1 ly Lucky Cat + 1 nước siêu lớn + 1 bắp ngọt lớn",
                price = 239000,
                quantity = 0,
                foodID = "5"
            },
            new Food
            {
                foodName = "MY COMBO",
                image = "https://www.cgv.vn/media/concession/web/63aaa2d81b6bf_1672127192.png",
                detail = "1 nước siêu lớn + 1 bắp ngọt lớn",
                price = 85000,
                quantity = 0,
                foodID = "6"
            },
            new Food
            {
                foodName = "SMILE COMBO",
                image = "https://www.cgv.vn/media/concession/web/63aaa31525d4c_1672127253.png",
                detail = "2 nước siêu lớn + 1 bắp ngọt lớn",
                price = 109000,
                quantity = 0,
                foodID = "7"
            },
        };
        }
    }

}
