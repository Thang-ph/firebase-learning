using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Firebase
{
    public class EditModel : PageModel
    {
        private readonly IFirestoreService _firestoreService;
        private readonly IFirebaseStorageService _storageService;
        [BindProperty]
        public ShoeDto Shoe { get; set; }
        
        public Shoe OldShoe {  get; set; }

        public EditModel(IFirestoreService firestoreService, IFirebaseStorageService storageService)
        {
            _firestoreService = firestoreService;
            _storageService = storageService;
            
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            OldShoe = await _firestoreService.GetById(id);

                       return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
          
            Uri videoUri = null;
            var currentShoe = await _firestoreService.GetById(id);
            if (Shoe.Video != null)
            {
                videoUri= await _storageService.UploadFile(Shoe.Name, Shoe.Video);
             
            }
            Shoe updateShoe=new Shoe() { Id=id,
            Name= Shoe.Name ?? currentShoe.Name,
                Brand =Shoe.Brand,
            Price = Shoe.Price != null ? Shoe.Price : currentShoe.Price,
                VideoUri = videoUri != null ? videoUri : currentShoe.VideoUri
            };
            await _firestoreService.Update(updateShoe);
            return RedirectToPage("Index");
        }

    }
}
