 namespace Company.G02.PL.Helpers
{
    public static class DocumentsSetting
    {
        public static string UploudFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // تأكد من أن المجلد موجود
            }

            var fileExtension = Path.GetExtension(file.FileName); // استخراج الامتداد الصحيح
            var fileName = $"{Guid.NewGuid()}{fileExtension}"; // اسم فريد مع الامتداد
            var filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fileName; // ✅ الآن يتم إرجاع اسم الملف الصحيح
        }



        public static void DeleteFile(string fileName, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            var filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
