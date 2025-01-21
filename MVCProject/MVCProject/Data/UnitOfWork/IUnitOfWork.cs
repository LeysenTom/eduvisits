using Microsoft.EntityFrameworkCore;
using MVCProject.Data.repository;
using MVCProject.Models;

namespace MVCProject.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Afwezigheid> AfwezigheidRepository { get; }
        IRepository<FotoAlbum> FotoalbumRepository { get; }
        IRepository<Gebruiker> GebruikerRepository { get; }
        IRepository<Klas> KlasRepository { get; }
        IRepository<Navorming> NavormingRepository { get; }
        IRepository<Studiebezoek> StudiebezoekRepository { get; }
        IRepository<Vak> VakRepository { get; }
        IRepository<Begeleiding> BegeleidingRepository { get; }
        IRepository<Bijlage> BijlageRepository { get; }
        IRepository<Foto> FotoRepository { get; }
        IRepository<GebruikerNavorming> GebruikerNavormingRepository { get; }
        IRepository<KlasStudiebezoek> KlasStudiebezoekRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
        Task<Gebruiker> GetUserByEmail(string email);
    }
}