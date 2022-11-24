Add-Migration -Context DataDbContext -o Migrations\Data Init 
Update-Database -Context DataDbContext

Add-Migration -Context ApplicationDbContext -o Migrations\Identity Init 
Update-Database -Context ApplicationDbContext