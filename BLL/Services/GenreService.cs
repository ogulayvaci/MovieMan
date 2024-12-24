using BLL.Context;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class GenreService: Service, IService<genre, GenreModel>
{
    public GenreService(Db db) : base(db)
    {
    }

    public IQueryable<GenreModel> Query()
    {
        return _db.genre.OrderBy(g => g.name).Select(g => new GenreModel() { Record = g });
    }

    public Service Create(genre entity)
    {
        if (_db.genre.Any(g => g.name == entity.name))
            return Error("A genre with that name already exists");
        _db.genre.Add(entity);
        _db.SaveChanges();
        return Success("A new genre has been created");
    }

    public Service Update(genre entity)
    {
        if(_db.genre.Any(g => g.id != entity.id && g.name.ToLower() == entity.name.ToLower().Trim()))
            return Error("This Genre already exists.");
        var rec = _db.genre.SingleOrDefault(g=>g.id == entity.id );
        rec.name = entity.name.Trim();
        _db.genre.Update(rec);
        _db.SaveChanges();
        return Success("The genre has been updated");
    }

    public Service Delete(int id)
    {
        var rec = _db.genre.Include(g=>g.moviegenre).SingleOrDefault(d=> d.id == id);
        if(rec == null)
            return Error("This Genre does not exist.");
        if(rec.moviegenre.Any())
            return Error("This Genre has movie record(s).");
        _db.genre.Remove(rec);
        _db.SaveChanges();
        return Success("The genre has been deleted");
    }
}