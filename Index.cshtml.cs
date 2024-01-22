using Google.Apis.Storage.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Firebase
{
    public class IndexModel : PageModel
    {
        private readonly IFirestoreService _firestoreService;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public List<Shoe>? Shoes;

        public IndexModel(IFirestoreService firestoreService)
        {
            _firestoreService = firestoreService;
        }

        public async Task OnGetAsync()
        {

            Shoes = await _firestoreService.GetAll();
        }
      
    }
}
