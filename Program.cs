using (var db = new AppDbContext()){
    db.EnsureCreated();
    Console.WriteLine("База данных готова!");
}