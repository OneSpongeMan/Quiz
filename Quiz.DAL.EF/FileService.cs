using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DAL.EF
{
    internal class FileService
    {
        public FileService() { }

        public void Write(string filePath, byte[] data)
        {
            File.WriteAllBytes(filePath, data);
        }
    }
}
