using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dsplit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length!=4)
            {
                Console.WriteLine("Es müssen 3 Parameter angeben werden");

            }
            else
            {
                int max = 0;
                ulong bytesize = Convert.ToUInt64(args[1]);
                ulong startpos = Convert.ToUInt64(args[0]);
                if (args[2].ToLower()!="end" && args[2].ToLower()!="max")
                {
                    max = Convert.ToInt32(args[2]);
                }
                string filename = args[3];
                int p = filename.LastIndexOf(@"\");
                string file = filename.Substring(p+1);
                int p1 = file.LastIndexOf(".");
                string end = file.Substring(p1);
                file = file.Remove(p1);
               
                Directory.CreateDirectory(Directory.GetCurrentDirectory()+@"\splittedfiles");
                int a = 0;
                using (BinaryReader r = new BinaryReader(File.OpenRead(filename), Encoding.Unicode, true))
                {
                    if (max != 0)
                    {


                        for (ulong i = startpos; i < Convert.ToUInt64(max); i += bytesize)
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
                                    a++;
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + a.ToString() + end, FileMode.Create, FileAccess.Write))
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
                                    a++;
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + a.ToString() + end, FileMode.Create, FileAccess.Write))
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
                                    a++;
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + a.ToString() + end, FileMode.Create, FileAccess.Write))
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
                                    a++;
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + a.ToString() + end, FileMode.Create, FileAccess.Write))
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
}
