using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DNASerializer
{
    class DNASerializer
    {
        String pathOfExecutable = AppDomain.CurrentDomain.BaseDirectory + "/";
        //Compresses and Decompresses a String containing the following letters:
        // A = Adenine - 00
        // T = Thymine - 01
        // G = Guanine - 10
        // C = Cytosine - 11

        public static BitArray Compress(string DNA)
        {
            DNA = DNA.ToUpper();
            char[] charArray = DNA.ToCharArray();
            bool[] boolarray = new bool[DNA.Length * 2];

            for (int i = 0; i < charArray.Length - 1; i++)
            {
                bool[] baseInBools = ConvertToBoolArray(charArray[i]);
                boolarray[i * 2] = baseInBools[0];
                boolarray[i * 2 + 1] = baseInBools[1];
            }
            return new BitArray(boolarray);
        }

        private static bool[] ConvertToBoolArray(char c)
        {
            switch (c)
            {
                case 'A':
                    bool[] aBools = { false, false };
                    return aBools;
                case 'T':
                    bool[] tBools = { false, true };
                    return tBools;
                case 'G':
                    bool[] gBools = { true, false };
                    return gBools;
                case 'C':
                    bool[] cBools = { true, true };
                    return cBools;
            }
            return new bool[0];
        }


        public static string Decompress(BitArray compressedDNA)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < compressedDNA.Length - 1; i += 2)
            {
                if (compressedDNA[i] == false)
                {
                    if (compressedDNA[i + 1] == false)
                        sb.Append("A");
                    else
                        sb.Append("T");
                }
                else
                {
                    if (compressedDNA[i + 1] == false)
                        sb.Append("G");
                    else
                        sb.Append("C");
                }
            }
            return sb.ToString();
        }

        public static void Serialize(BitArray bitArray, String filename)
        {
            Stream stream = new FileStream(pathOfExecutable + filename, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, bitArray);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                stream.Close();
            }
        }
        public static BitArray Deserialize(String filename)
        {
            Stream stream = new FileStream(pathOfExecutable + filename, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            BitArray bitArray = new BitArray(new bool[0]);
            try
            {
                bitArray = (BitArray)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                stream.Close();
                
            }
            return bitArray;
        }
    }
}
