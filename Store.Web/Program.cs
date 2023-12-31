
using Store;
using Store.Contractors;
using Store.Memory;
using Store.Messages;
using Store.Web.App;
using Store.Web.Contractors;
using Store.YandexKassa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddSingleton<BookService>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<INotificationService, DebugNotificationService>();
builder.Services.AddSingleton<IDeliveryService, PostamateDeliveryService>();
builder.Services.AddSingleton<IPaymentService, CashPaymentService>();
builder.Services.AddSingleton<IPaymentService, YandexKassaPaymentService>();
builder.Services.AddSingleton<IWebContractorService, YandexKassaPaymentService>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
      );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"

    );



app.Run();
