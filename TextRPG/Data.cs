using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Data
    {
        public char[] mapCharArr = new char[]
        {
            //0 빈공간
            ' ',
            //1 벽
            '■',
            //2 포탈
            '※'

        };
        public Dictionary<ItemID, string> dictItemName, dictItemDesc;
        public Dictionary<ItemID, Item> dictItem;

        private static Data instance;
        private Data()
        {
            dictItem = new Dictionary<ItemID, Item>();
            dictItemDesc = new Dictionary<ItemID, string>();
            dictItemName = new Dictionary<ItemID, string>();

            //소비 아이템--------------------------------------------------------
            dictItem.Add(ItemID.RED_POTION, new RedPotion());
            dictItemName.Add(ItemID.RED_POTION, "빨간 포션");
            dictItemDesc.Add(ItemID.RED_POTION, "맛은 없을 것 같다");


            //장비 아이템--------------------------------------------------------
            dictItem.Add(ItemID.LONG_SWORD, new LongSword());
            dictItemName.Add(ItemID.LONG_SWORD, "롱 소드");
            dictItemDesc.Add(ItemID.LONG_SWORD, "날카로운 검이다");

            dictItem.Add(ItemID.LETHER_GLOVE, new LeatherGlove());
            dictItemName.Add(ItemID.LETHER_GLOVE, "가죽 장갑");
            dictItemDesc.Add(ItemID.LETHER_GLOVE, "질긴 가죽으로 만들었다");

            //기타 아이템---------------------------------------------------------
            dictItem.Add(ItemID.SLIME_MUCUS, new SlimeMucus());
            dictItemName.Add(ItemID.SLIME_MUCUS, "슬라임 점액");
            dictItemDesc.Add(ItemID.SLIME_MUCUS, "어딘가 쓸모가 있을지도 모른다");
        }
        public static Data Instance {
            get
            {
                if(instance == null)
                {
                    instance = new Data();
                }
                return instance;
            }
        }
    }
}
