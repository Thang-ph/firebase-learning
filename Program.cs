using Firebase;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Code\Firebase\studportal-4fbba-firebase-adminsdk-50a34-f3c0810229.json");
builder.Services.AddSingleton<IFirestoreService>(s => new FirestoreService(
    FirestoreDb.Create("studportal-4fbba")
    ));
builder.Services.AddSingleton<IFirebaseStorageService, FirebaseStorageService>(
    s => new FirebaseStorageService(StorageClient.Create())
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
