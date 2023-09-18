namespace CarAnnouncementProject.Extensions.FileExtension
{
    public static class FileExtention
    {
        public static bool CheckFileFormat(this List<IFormFile> files, string type)
        {
            bool returnType = true;
            foreach (IFormFile file in files)
            {
                returnType = file.ContentType.Contains(type);
            }
            return returnType;
        }
        public static bool CheckFileSize(this List<IFormFile> files, int size)
        {
            bool returnType = true;
            foreach (IFormFile file in files)
            {
                returnType = file.Length / 1024 > size;
            }
            return returnType;
        }

        public static async Task<string> SaveFileAsync(this IFormFile file, string wwwroot, params string[] folders)
        {
            string resultPath = wwwroot;
            string filename = Guid.NewGuid().ToString() + file.FileName;
            foreach (var folder in folders)
            {
                resultPath = Path.Combine(resultPath, folder);
            };
            resultPath = Path.Combine(resultPath, filename);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);

            }
            return filename;
        }
    }
}
