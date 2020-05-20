using CadastroProduto.Library.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProduto.Business.Utils
{
    public static class ManagerImage
    {
        private static byte[] ConvertIFormFileToBytes(IFormFile file)
        {
            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return fileBytes;
        }

        private static bool ImageFileIsValid(byte[] file)
        {
            return GetImageFormat(file) != ImageFormat.UNKNOWN;
        }

        private static ImageFormat GetImageFormat(byte[] bytes)
        {
            var response = ImageFormat.UNKNOWN;

            var jpeg = new byte[] { 255, 216, 255, 224 };// jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };// jpeg canon

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            {
                response = ImageFormat.JPEG;
            }
            else if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
            {
                return ImageFormat.JPEG;
            }

            return response;
        }

        public static async Task<string> SaveFileAsync(IFormFile file)
        {
            string fileName;

            if (file == null)
            {
                throw new Exception("Imagem não fornecida");
            }

            var fileBytes = ConvertIFormFileToBytes(file);

            if (!ImageFileIsValid(fileBytes))
            {
                throw new Exception("Tipo de imagem não suportado");
            }

            var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            fileName = $"{Guid.NewGuid()}.{extension}";

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/images-products"))
            {
                DirectoryInfo di = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/images-products");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "images-products", fileName);
           

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }

        public static async Task<string> GetFileAsync(string fullPath)
        {
            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                if (File.Exists(fullPath))
                {
                    var bytes = await File.ReadAllBytesAsync(fullPath);
                    var imageBase64 = Convert.ToBase64String(bytes);

                    return "data:image/jpg;base64," + imageBase64;
                }
            }

            return "";
        }

        public static void DeleteFile(string fullPath)
        {
            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }
}
