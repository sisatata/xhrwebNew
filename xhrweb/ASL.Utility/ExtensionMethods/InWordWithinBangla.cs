using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Utility.ExtensionMethods
{
    public class InWordWithinBangla
    {
        private static string ConvertDigitToString(int digits)
        {
            switch (digits)
            {
                case 0: return "";
                case 1: return "এক";
                case 2: return "দুই";
                case 3: return "তিন";
                case 4: return "চার";
                case 5: return "পাচ";
                case 6: return "ছয়";
                case 7: return "সাত";
                case 8: return "আট";
                case 9: return "নয়";
                case 10: return "দশ";
                case 11: return "এগারো";
                case 12: return "বারো";
                case 13: return "তেরো";
                case 14: return "চোদ্দ";
                case 15: return "পনেরো";
                case 16: return "ষোল";
                case 17: return "সতেরো";
                case 18: return "আঠারো";
                case 19: return "উনিশ";
                case 20: return "কুড়ি";
                case 21: return "একুশ";
                case 22: return "বাইশ";
                case 23: return "তেইশ";
                case 24: return "চব্বিশ";
                case 25: return "পঁচিশ";
                case 26: return "ছাব্বিশ";
                case 27: return "সাতাশ";
                case 28: return "আঠাশ";
                case 29: return "ঊনত্রিশ";
                case 30: return "ত্রিশ";
                case 31: return "একত্রিশ";
                case 32: return "বত্রিশ";
                case 33: return "তেত্রিশ";
                case 34: return "চৌত্রিশ";
                case 35: return "পঁয়ত্রিশ";
                case 36: return "ছত্রিশ";
                case 37: return "সাঁয়ত্রিশ";
                case 38: return "আটত্রিশ";
                case 39: return "ঊনচল্লিশ";
                case 40: return "চল্লিশ";
                case 41: return "একচল্লিশ";
                case 42: return "বিয়াল্লিশ";
                case 43: return "তেতাল্লিশ";
                case 44: return "চুয়াল্লিশ";
                case 45: return "পঁয়তাল্লিশ";
                case 46: return "ছেচল্লিশ";
                case 47: return "সাতচল্লিশ";
                case 48: return "আটচল্লিশ";
                case 49: return "উনপঞ্চাশ";
                case 50: return "পঞ্চাশ";
                case 51: return "একান্ন";
                case 52: return "বায়ান্ন";
                case 53: return "তিপ্পান্ন";
                case 54: return "চুয়ান্ন";
                case 55: return "পঞ্চান্ন";
                case 56: return "ছাপ্পান্ন";
                case 57: return "সাতান্ন";
                case 58: return "আটান্ন";
                case 59: return "ঊনষাট";
                case 60: return "ষাট";
                case 61: return "একষট্টি";
                case 62: return "বাষট্টি";
                case 63: return "তেষট্টি";
                case 64: return "চৌষট্টি";
                case 65: return "পঁয়ষট্টি";
                case 66: return "ছেষট্টি";
                case 67: return "সাতষট্টি";
                case 68: return "আটষট্টি";
                case 69: return "ঊনসত্তর";
                case 70: return "সত্তর";
                case 71: return "একাত্তর";
                case 72: return "বাহাত্তর";
                case 73: return "তিয়াত্তর";
                case 74: return "চুয়াত্তর";
                case 75: return "পঁচাত্তর";
                case 76: return "ছিয়াত্তর";
                case 77: return "সাতাত্তর";
                case 78: return "আটাত্তর";
                case 79: return "ঊনআশি";
                case 80: return "আশি";
                case 81: return "একাশি";
                case 82: return "বিরাশি";
                case 83: return "তিরাশি";
                case 84: return "চুরাশি";
                case 85: return "পঁচাশি";
                case 86: return "ছিয়াশি";
                case 87: return "সাতাশি";
                case 88: return "অষ্টআশি";
                case 89: return "ঊননব্বই";
                case 90: return "নব্বই";
                case 91: return "একানব্বই";
                case 92: return "বিরানব্বই";
                case 93: return "তিরানব্বই";
                case 94: return "চুরানব্বই";
                case 95: return "পঁচানব্বই";
                case 96: return "ছিয়ানব্বই";
                case 97: return "সাতানব্বই";
                case 98: return "আটানব্বই";
                case 99: return "নিরানব্বই";
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} not a digit", digits));
            }
        }
        private static string ConvertDigitToStringForEnglish(int digits)
        {
            switch (digits)
            {
                case 0: return "";
                case 1: return "One";
                case 2: return "Two";
                case 3: return "Three";
                case 4: return "Four";
                case 5: return "Five";
                case 6: return "Six";
                case 7: return "Seven";
                case 8: return "Eight";
                case 9: return "Nine";
                case 10: return "Ten";
                case 11: return "Eleven";
                case 12: return "Twelve";
                case 13: return "Thirteen";
                case 14: return "Fourteen";
                case 15: return "Fifteen";
                case 16: return "Sixteen";
                case 17: return "Seventeen";
                case 18: return "Eighteen";
                case 19: return "Nineteen";
                case 20: return "Twenty";
                case 21: return "Twenty-One";
                case 22: return "Twenty-Two";
                case 23: return "Twenty-Three";
                case 24: return "Twenty-Four";
                case 25: return "Twenty-Five";
                case 26: return "Twenty-Six";
                case 27: return "Twenty-Seven";
                case 28: return "Twenty-Eight";
                case 29: return "Twenty-Nine";
                case 30: return "Thirty";
                case 31: return "Thirty-One";
                case 32: return "Thirty-Two";
                case 33: return "Thirty-Three";
                case 34: return "Thirty-Four";
                case 35: return "Thirty-Five";
                case 36: return "Thirty-Six";
                case 37: return "Thirty-Seven";
                case 38: return "Thirty-Eight";
                case 39: return "Thirty-Nine";
                case 40: return "Forty";
                case 41: return "Forty-One";
                case 42: return "Forty-Two";
                case 43: return "Forty-Three";
                case 44: return "Forty-Four";
                case 45: return "Forty-Five";
                case 46: return "Forty-Six";
                case 47: return "Forty-Seven";
                case 48: return "Forty-Eight";
                case 49: return "Forty-Nine";
                case 50: return "Fifty";
                case 51: return "Fifty-One";
                case 52: return "Fifty-Two";
                case 53: return "Fifty-Three";
                case 54: return "Fifty-Four";
                case 55: return "Fifty-Five";
                case 56: return "Fifty-Six";
                case 57: return "Fifty-Seven";
                case 58: return "Fifty-Eight";
                case 59: return "Fifty-Nine";
                case 60: return "Sixty";
                case 61: return "Sixty-One";
                case 62: return "Sixty-Two";
                case 63: return "Sixty-Three";
                case 64: return "Sixty-Four";
                case 65: return "Sixty-Five";
                case 66: return "Sixty-Six";
                case 67: return "Sixty-Seven";
                case 68: return "Sixty-Eight";
                case 69: return "Sixty-Nine";
                case 70: return "Seventy";
                case 71: return "Seventy-One";
                case 72: return "Seventy-Two";
                case 73: return "Seventy-Three";
                case 74: return "Seventy-Four";
                case 75: return "Seventy-Five";
                case 76: return "Seventy-Six";
                case 77: return "Seventy-Seven";
                case 78: return "Seventy-Eight";
                case 79: return "Seventy-Nine";
                case 80: return "Eighty";
                case 81: return "Eighty-One";
                case 82: return "Eighty-Two";
                case 83: return "Eighty-Three";
                case 84: return "Eighty-Four";
                case 85: return "Eighty-Five";
                case 86: return "Eighty-Six";
                case 87: return "Eighty-Seven";
                case 88: return "Eighty-Eight";
                case 89: return "Eighty-Nine";
                case 90: return "Ninety";
                case 91: return "Ninety-One";
                case 92: return "Ninety-Two";
                case 93: return "Ninety-Three";
                case 94: return "Ninety-Four";
                case 95: return "Ninety-Five";
                case 96: return "Ninety-Six";
                case 97: return "Ninety-Seven";
                case 98: return "Ninety-Eight";
                case 99: return "Ninety-Nine";
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} not a digit", digits));
            }
        }
        private static string ConvertBigNumberToString(int n, int baseNum, string baseNumStr)
        {
            //string separator = (baseNumStr != "শত") ? ", " : " ";
            string separator = "";
            if (baseNumStr != "শত ")
            {
                separator = " ";
            }
            int bigPart = (int)(Math.Floor((double)n / baseNum));
            string bigPartStr = ConvertNumbertoWords(bigPart) + " " + baseNumStr;
            if (n % baseNum == 0) return bigPartStr;
            int restOfNumber = n - bigPart * baseNum;
            return bigPartStr + separator + ConvertNumbertoWords(restOfNumber);
        }
        private static string ConvertBigEngLishNumberToString(int n, int baseNum, string baseNumStr)
        {
            string separator = "";
            if (baseNumStr != "Hundred ")
            {
                separator = " ";
            }
            int bigPart = (int)(Math.Floor((double)n / baseNum));
            string bigPartStr = ConvertNumbertoEnglishWords(bigPart) + " " + baseNumStr;
            if (n % baseNum == 0) return bigPartStr;
            int restOfNumber = n - bigPart * baseNum;
            return bigPartStr + separator + ConvertNumbertoEnglishWords(restOfNumber);
        }
        public static string ConvertNumbertoWords(int number)
        {
            if (number < 0)
                throw new NotSupportedException("negative numbers not supported");
            if (number == 0)
                return "";
            if (number < 100)
                return ConvertDigitToString(number);
            if (number < 1000)
                return ConvertBigNumberToString(number, (int)1e2, "শত ");

            if (number < 1e5)
                return ConvertBigNumberToString(number, (int)1e3, "হাজার");
            if (number < 1e7)
                return ConvertBigNumberToString(number, (int)1e5, "লক্ষ");
            if (number < 1e9)
                return ConvertBigNumberToString(number, (int)1e7, "কোটি");
            //if (n < 1e11)
            return ConvertBigNumberToString(number, (int)1e9, "কোটি");
        }
        public static string ConvertNumbertoEnglishWords(int number)
        {
            if (number < 0)
                throw new NotSupportedException("negative numbers not supported");
            if (number == 0)
                return "";
            if (number < 100)
                return ConvertDigitToStringForEnglish(number);
            if (number < 1000)
                return ConvertBigEngLishNumberToString(number, (int)1e2, "Hundred ");

            if (number < 1e5)
                return ConvertBigEngLishNumberToString(number, (int)1e3, "Thousand");
            if (number < 1e7)
                return ConvertBigEngLishNumberToString(number, (int)1e5, "Lac");
            if (number < 1e9)
                return ConvertBigEngLishNumberToString(number, (int)1e7, "Crore");
            //if (n < 1e11)
            return ConvertBigEngLishNumberToString(number, (int)1e9, "Crore");
        }
        public static string ConvertNumbertoWords2(int number)
        {
            if (number < 0)
                throw new NotSupportedException("negative numbers not supported");
            if (number == 0)
                return "";
            if (number < 100)
                return ConvertDigitToString(number);
            if (number < 1000)
                return ConvertBigNumberToString(number, (int)1e2, "শত ");

            if (number < 1e5)
                return ConvertBigNumberToString(number, (int)1e3, "হাজার");
            if (number < 1e7)
                return ConvertBigNumberToString(number, (int)1e5, "লক্ষ");
            if (number < 1e9)
                return ConvertBigNumberToString(number, (int)1e7, "কোটি");
            //if (n < 1e11)
            return ConvertBigNumberToString(number, (int)1e9, "কোটি");
        }
    }
}
