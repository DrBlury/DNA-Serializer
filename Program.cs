using System;
using System.Collections;

namespace DNASerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Serialisierung des BitArrays ab einer DNA Länge von 150 Basenpaaren sinnvoll.
            string DNA = "AGCAGCGAGCAGTCAGCAGCAGCATCAGCACTACTACTACGCTAGCTAGCATGCATGCATGCTAGCTAGCATCGATGCATGCATCAGCACGATGCATCGAAGCAGCGAGCAGTCAGCAGCAGCATCAGCACTACTACTACACGCAGCGTAA";

            BitArray compressedDNA = DNASerializer.Compress(DNA);
            Console.WriteLine("Compressed DNA.");
            DNASerializer.serialize(compressedDNA, "testfile.DNA");
            Console.WriteLine("Serialized DNA.");
            BitArray deserializedDNA = DNASerializer.deserialize("testfile.DNA");
            Console.WriteLine("Deserialized DNA.");
            String decompressedDNA = DNASerializer.DeCompress(deserializedDNA);

            Console.WriteLine(decompressedDNA);

        }
    }
}
