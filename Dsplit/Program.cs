/* This program is published under the  GNU GENERAL PUBLIC LICENSE!
 * Copyright (C) 1989, 1991 Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 * Everyone is permitted to copy and distribute verbatim copies
 * of this license document, but changing it is not allowed.
 * 
 * For more information search the GNU GENERAL PUBLIC LICENSE on the internet!
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // For reading and writing files

namespace Dsplit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length!=4)                                             // throws a error output if the wrong number of argument given
            {                                                               
                Console.WriteLine("Es müssen 3 Parameter angeben werden");
                                                                                    
            }
            else
            {
                int max = 0;
                ulong bytesize = Convert.ToUInt64(args[1]);                // size splitting packets
                ulong startpos = Convert.ToUInt64(args[0]);                // position to start splitting
                if (args[2].ToLower()!="end" && args[2].ToLower()!="max")  // Check that the user want to split until the end or not
                {                                               
                    max = Convert.ToInt32(args[2]);
                }
                string filename = args[3];                                 // The path and name of the file that should be splitted
                int p = filename.LastIndexOf(@"\");
                string file = filename.Substring(p+1);
                int p1 = file.LastIndexOf(".");
                string end = file.Substring(p1);                           // Get the fileextension
                file = file.Remove(p1);
               
                Directory.CreateDirectory(Directory.GetCurrentDirectory()+@"\splittedfiles"); // Create the Direcoty where the files will be saved
                int a = 0;
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
                                    a++;                                                               
                                    using (FileStream f = new FileStream(Directory.GetCurrentDirectory() + @"\splittedfiles\" + file + a.ToString() + end, FileMode.Create, FileAccess.Write))
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
