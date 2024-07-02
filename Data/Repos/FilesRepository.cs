using Microsoft.EntityFrameworkCore;
using PolyglotAPI.Data.Models;

namespace PolyglotAPI.Data.Repos
{
    public interface IFilesRepository
    {
        Task UploadImagesAsync(List<Image> images);
        Task UploadAudioAsync(List<Audio> audios);
        Task<IEnumerable<Audio>> GetAllAudiosAsync();
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task<Audio?> GetAudioByIdAsync(int id);
        Task<Image?> GetImageByIdAsync(int id);
    }
    public class FilesRepository : IFilesRepository
    {
        private readonly ApplicationDbContext _context;

        public FilesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UploadImagesAsync(List<Image> images)
        {
            if (images != null && images.Count > 0)
            {
                _context.Images.AddRange(images);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UploadAudioAsync(List<Audio> audios)
        {
            if (audios != null && audios.Count > 0)
            {
                foreach (var audio in audios)
                {
                    if (!_context.Audios.Any(a => a.Id == audio.Id && a.LanguageCode == audio.LanguageCode))
                    {
                        _context.Audios.Add(audio);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Audio>> GetAllAudiosAsync()
        {
            return await _context.Audios.ToListAsync();
        }

        public async Task<IEnumerable<Image>> GetAllImagesAsync()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Audio?> GetAudioByIdAsync(int id)
        {
            return await _context.Audios.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Image?> GetImageByIdAsync(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
