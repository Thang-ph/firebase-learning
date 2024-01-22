using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Firebase
{
    public class CreateModel : PageModel
    {
        private readonly IFirestoreService _firestoreService;
        private readonly IFirebaseStorageService _storageService;
        
        [BindProperty]
        public ShoeDto Shoe { get; set; }

        public CreateModel(IFirestoreService firestoreService, IFirebaseStorageService storageService)
        {
            _firestoreService = firestoreService;
            _storageService = storageService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var videoUri = await _storageService.UploadFile(Shoe.Name, Shoe.Video);

            await _firestoreService.Add(new Shoe
            {
                Name = Shoe.Name,
                Brand = Shoe.Brand,
                Price = Shoe.Price,
                VideoUri = videoUri,
            });

            return RedirectToPage("Index");
        }
    }
}
