using BLL.Context;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class MovieService : Service, IService<movie, MovieModel>
{
    public MovieService(Db db) : base(db)
    {
    }

    public IQueryable<MovieModel> Query()
    {
        return _db.movie
            .Include(m => m.director)
            .Include(m => m.moviegenre)
            .ThenInclude(mg=>mg.genre)
            .OrderBy(m => m.name)
            .ThenBy(m => m.releasedate)
            .Select(m => new MovieModel { Record = m });
    }

    public Service Create(movie entity)
    {
        if (_db.movie.Any(m => m.name.Trim().ToLower() == entity.name.Trim().ToLower()))
            return Error("Movie already exists");
        _db.movie.Add(entity);
        _db.SaveChanges();
        return Success("Movie created");
    }

    public Service Update(movie entity)
    {
        if (_db.movie.Any(m => m.id != entity.id && m.name.Trim().ToLower() == entity.name.Trim().ToLower()))
            return Error("Movie with the same name already exists");
        var rec = _db.movie.Include(m=>m.moviegenre).SingleOrDefault(m=>m.id == entity.id);
        if (rec is null)
            return Error("Record not found");
        _db.moviegenre.RemoveRange(rec.moviegenre);
        rec.name = entity.name.Trim();
        rec.releasedate = entity.releasedate;
        rec.directorid = entity.directorid;
        rec.moviegenre = entity.moviegenre;
        rec.totalrevenue = entity.totalrevenue;
        _db.movie.Update(rec);
        _db.SaveChanges();
        return Success("Movie updated");
    }

    public Service Delete(int id)
    {
        var rec = _db.movie.Include(m=>m.moviegenre).FirstOrDefault(m => m.id == id);
        if(rec is null)
            return Error("Movie not found");
        _db.moviegenre.RemoveRange(rec.moviegenre);
        _db.movie.Remove(rec);
        _db.SaveChanges();
        return Success("Movie deleted");
    }
}