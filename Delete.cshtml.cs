using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Firebase
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }

        private readonly IFirebaseStorageService _storageService;
        private readonly IFirestoreService _firebaseService;

        public DeleteModel(IFirestoreService firestoreService, IFirebaseStorageService firebaseStorageService)
        {
             _firebaseService= firestoreService;
            _storageService = firebaseStorageService;

        }
        public async Task<IActionResult> OnGetAsync()
        {
           var Shoe=await _firebaseService.GetById(id);

            await _storageService.DeleteFile(Shoe.Name, Shoe.VideoUri);
            await _firebaseService.Delete(id);
           return RedirectToPage("Index");
        }
    }
}
