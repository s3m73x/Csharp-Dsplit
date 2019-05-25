using System;
using System.Text;
using System.IO; 

namespace Dsplit
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong bytesize=new ulong(); //The size of the spitted files

            ulong startpos = new ulong(); //The position to start splitting

            int max = 0; //The position to end splitting

            string filename = null; //The filename with complete path info

            if (args.Length != 4) //If no arguments given, the user has to type them in                                          
            {
                Console.Write("Type in the prefered size of the splitted files:");

                bytesize = Convert.ToUInt64(Console.ReadLine());

                Console.Write("Type in the potion where to start with splitting:");

                startpos = Convert.ToUInt64(Console.ReadLine());

                Console.Write("Should the file be split up to the end?:");

                string toend = Console.ReadLine().ToLower();

                if (toend != "yes")
                {
                    Console.Write("Type in the position where to end with splitting:");

                    max = Convert.ToInt32(Console.ReadLine());
                }

                Console.Write("Type in the filename with full path:");

                filename = Console.ReadLine();

            }
            else
            {


                bytesize = Convert.ToUInt64(args[1]);

                startpos = Convert.ToUInt64(args[0]);

                if (args[2].ToLower() != "end" && args[2].ToLower() != "max") 
                {
                    max = Convert.ToInt32(args[2]);
                }

                filename = args[3];                                 
            }

                int p = filename.LastIndexOf(@"\"); //The positione of the last Backslash

                string file = filename.Substring(p+1); //The filename without the path

                int p1 = file.LastIndexOf("."); //The position of the last dot

                string end = file.Substring(p1);   // Get the fileextension

                file = file.Remove(p1);

                Directory.SetCurrentDirectory(filename.Remove(filename.LastIndexOf(@"\")));
               
                Directory.CreateDirectory(Directory.GetCurrentDirectory()+@"\splittedfiles"); // Create the Direcoty where the files will be saved

                int byteblockcounter = 0;

                using (BinaryReader r = new BinaryReader(File.OpenRead(filename), Encoding.Unicode, true)) // Open the file to read
                {
                    if (max != 0)                                                                          // Make shure that the end position is greater than 0
                    {


                        for (ulong i = startpos; i < Convert.ToUInt64(max); i += bytesize)              
                        {

                            byte[] bytes = new byte[bytesize];
                            if (r.BaseStream.Position + Convert.ToInt32(bytesize) <= max)
                            {
                                for (ulong x = 0; x < bytesize; x++)
                                {

                                    bytes[x] = r.ReadByte();                                            // read the file bytewise and save it to the buffer




                                }
                                if (r.BaseStream.Position >= Convert.ToInt32(startpos))
                                {
                                    byteblockcounter++;                                                               
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + byteblockcounter.ToString() + end, FileMode.Create, FileAccess.Write))
                                    {
                                        f.Write(bytes, 0, Convert.ToInt32(bytesize)); // Write the buffer into a file
                                    }
                                    bytes = null;
                                }
                            }
                            else
                            {
                                int rest = Convert.ToInt32(r.BaseStream.Position) + Convert.ToInt32(bytesize) -max;
                                bytesize -= Convert.ToUInt64(rest);
                                for (ulong x = 0; x < bytesize; x++)
                                {

                                    bytes[x] = r.ReadByte();




                                }
                                if (r.BaseStream.Position >= Convert.ToInt32(startpos))
                                {
                                    byteblockcounter++;
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + byteblockcounter.ToString() + end, FileMode.Create, FileAccess.Write))
                                    {
                                        f.Write(bytes, 0, Convert.ToInt32(bytesize));
                                    }
                                    bytes = null;
                                }
                            }
                            
                           
                        }
                    }
                    else
                    {

                        for (ulong i = startpos; i <Convert.ToUInt64(r.BaseStream.Length); i += bytesize)
                        {

                            byte[] bytes = new byte[bytesize];
                            if (r.BaseStream.Position + Convert.ToInt32(bytesize) <= r.BaseStream.Length)
                            {
                                for (ulong x = 0; x < bytesize; x++)
                                {

                                    bytes[x] = r.ReadByte();




                                }
                                if (r.BaseStream.Position >= Convert.ToInt32(startpos))
                                {
                                    byteblockcounter++;
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + byteblockcounter.ToString() + end, FileMode.Create, FileAccess.Write))
                                    {
                                        f.Write(bytes, 0, Convert.ToInt32(bytesize));
                                    }
                                    bytes = null;
                                }
                            }
                            else
                            {
                                int rest = Convert.ToInt32(r.BaseStream.Position) + Convert.ToInt32(bytesize) - Convert.ToInt32(r.BaseStream.Length);
                                bytesize -= Convert.ToUInt64(rest);
                                for (ulong x = 0; x < bytesize; x++)
                                {

                                    bytes[x] = r.ReadByte();




                                }
                                if (r.BaseStream.Position >= Convert.ToInt32(startpos))
                                {
                                    byteblockcounter++;
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + byteblockcounter.ToString() + end, FileMode.Create, FileAccess.Write))
                                    {
                                        f.Write(bytes, 0, Convert.ToInt32(bytesize));
                                    }
                                    bytes = null;
                                }
                            }
                        }
                    }
                }

            }
            
        }
    }

