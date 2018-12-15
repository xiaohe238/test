using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZfbQrcode
{
   public class Method
    {
        string Lovercase = "abcdefghijklmnopqrstuvwxyz";
        string Capital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string Number = "0123456789";
        Random ran = new Random();

        //随机生成数字（字符串）
        public string Numbers(int amount)
        {
            //int lov_mark=ran.Next(0,26);
            //int cap_mark=ran.Next(0,26);

            string numbers = "";
            for (int i = 0; i < amount; i++)
            {
                //字符串随机下标
                int num_mark = ran.Next(0, Convert.ToInt32(Number.Length));
                string str = Number[num_mark].ToString();//获取随机下标的字符，转化为字符串
                numbers += str;//拼接字符串
            }
            return numbers;
        }

        //获得指定位数的字符串（大小写字母与数字组合）
        public string LetterNumber(int amount)
        {
            string letnum = Lovercase + Capital + Number;
            string lu = "";
            for (int i = 0; i < amount; i++)
            {
                string str = letnum[ran.Next(0, Convert.ToInt32(letnum.Length))].ToString();
                lu += str;
            }
            return lu;
        }

        //生成手机号
        public string phone(int amount)
        {
            string phonenum = "1" + "3578"[ran.Next(0, 4)] + Numbers(amount - 2);
            return phonenum;
        }

        //非手机号规则数字
        public string numbphone(int amount)
        {
            string numb;
            do { numb = Numbers(amount); }
            while (numb[0] == '1' && (numb[1] == '3' || numb[1] == '5' || numb[1] == '7' || numb[1] == '8'));//非手机号规则
            //while(numb[0]!='1'||(numb[1]!='3'&&numb[1]!='5'&&numb[1]!='7'&&numb[1]!='8'));//手机号规则
            return numb;


        }
    }
}
