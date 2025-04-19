# ❓ Zašto je važno pisati README i upute za pokretanje?

U realnim softverskim projektima, dokumentacija kao što je `README.md` je **nužnost**.  
Evo nekoliko razloga zašto:

1. **Novi članovi tima**  
   Često se dešava da novi programeri dolaze u tim, a stari odlaze.  
   Novi član mora brzo shvatiti kako da pokrene aplikaciju i počne raditi – bez gubljenja dana na pogreške i pitanja.

2. **Vraćanje na projekat nakon pauze**  
   Možda će glavni developer pauzirati nekoliko mjeseci na projektu.  
   Bez dokumentacije će zaboraviti detalje poput:
   - koji DB kontekst se koristi
   - gdje je API endpoint za seed
   - koji port koristi Angular itd.

3. **Profesionalnost i standardi industrije**  
   U ozbiljnim firmama, svaki projekat mora imati dokumentaciju.  
   README je najosnovniji dokument koji se očekuje u svakom repozitoriju.

4. **DevOps / CI-CD kompatibilnost**  
   Kada se pravi automatski deployment (npr. preko GitHub Actions ili GitLaba), dokumentacija pomaže i u konfiguraciji build okruženja.

---

## ✅ Za koga se piše README?

README se piše za **developera koji zna**:
- kako koristiti osnovne alate: `.NET CLI`, `Angular CLI`, `npm`, `EF Core`, `Docker CLI`, itd.
- kako pokrenuti lokalni server, terminal, instalirati pakete

Ali **ne zna ništa o konkretnom projektu** – prvi put se susreće sa vašim repozitorijem.

# README.MD – primjer readme fajla

Glavni readme fajl se treba nalaziti na root repozitoriju


# 📘 Naziv projekta

Ovo je web aplikacija razvijena pomoću **.NET Core Web API** za backend i **Angular** za frontend.  
Aplikacija služi za ____________ (navesti svrhu – npr. vođenje evidencije korisnika, upravljanje zadacima, evidenciju prisutnosti itd.).

---




## ⚙️ Tehnologije i preduvjeti

Prije pokretanja aplikacije potrebno je instalirati, npr:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- SQL Server, Postgres (lokalni ili mrežni) ili Docker Desktop ver. ____

---

## 🔧 Backend – Pokretanje API servera

### 1. Pokretanje docker kontejnera za bazu 

Ako se koristi DB server preko docker konfiguracije kao dio projekta.

Pokrenuti Docker za DB server.

Otvoriti developer PowerShell (ili neki drugi terminal), pozicionirati se u root direktorij projekta i izvršiti:

```bash
docker compose up -d
```

> `-d` znači "detached mode" – servis ostaje aktivan i kada se terminal zatvori.

Za zaustavljanje:

```bash
docker compose down
```

---

### 2. Postavljanje baze podataka (EF migracije)

Ako se koristi EF migracije:

1. Postaviti `NazivProjekta.Api` kao **Startup Project** (iz ovog projekta se koristi connection string)
2. U `Package Manager Console` postaviti `NazivProjekta.Infrastructure` kao **Default project** (u ovom projektu se nalazi EF migration)
3. Provjeriti ili postaviti environment, jer različiti environmenti imaju različite konfiguracije koje se preuzimaju iz
`appsettings.Development.json` ili `appsettings.Staging.json`
itd.

Promjena environmenta 

```Package Manager Console
$env:ASPNETCORE_ENVIRONMENT='Development'
```

ili

```Package Manager Console
$env:ASPNETCORE_ENVIRONMENT='Staging'
```

Da li korisnik mora ručno postaviti connection string ako koristi DB server preko dockera ili dovoljno samo pokrenuti docker i api?

4. Ažurirati bazu:

```Package Manager Console
update-database
```

Ako postoji više DB konteksta:

```Package Manager Console
update-database -context GlavniDbContext
update-database -context NekiDrugiDbContext
```

---

### 3. Postavljanje baze putem SQL skripte ili .bak fajla

Ako se koristi `.sql` skripta ili `.bak` fajl, navesti:

- Lokaciju fajla
- Da li fajl sadrži strukturu i/ili ili uključuje seeder podatke

---

### 4. Seeder podaci

Provjeriti:

- Da li su podaci u `.sql` fajlu ili `.bak` fajlu
- Da li su podaci u EF migracijama - tj koristi se `HasData()` u `OnModelCreating()` (moguće kompletana seed ovdje importovati, samo ipak preporučuje samo za statičke podatke - tj. za podatke o kojima je opvisan code, npr user roles)
- Da li su podaci u servisi npr. `IMyDbInitializer` koji se automatski pokreće u `Program.cs`
- Da li su podaci u endpointu i koja je adresa, npr.

```
GET https://localhost:7100/api/devtools/seed
```

- Da li je endpoint zaštićen autorizacijom
- Šta se dešava pri višestrukom seedanju
- Da li se generiše random lozinka za korisnike, npr. admin

---

### 5. Pokretanje backend aplikacija

Navesti koje aplikacije se pokreću:

#### Backend Apps (portovi 7xxx)

- `https://localhost:7100` – API
- `https://localhost:7200` – SSO / Identity server
- `https://localhost:7300` – CDN / fajl server
- `https://localhost:7400` – Jobs kao web app
- `console app` – Jobs kao background servis

#### Dev Apps (portovi 8xxx)

- `https://localhost:8100` – Reports
- `https://localhost:8200` – Hangfire

> **Napomena**: Prije pokretanja `NazivProjekta.Jobs`, kreirati bazu `naziv_projekta_jobs_dev`. Hangfire neće automatski kreirati bazu.

Provjeriti SSL certifikate ako su potrebni.  
Provjeriti konfiguraciju osjetljivih podataka (`appsettings.Development.json`, `appsettings.Staging.json`, itd.)

---

## 🏗 Build

Primjeri build komandi:

```bash
.\build.ps1 -ProjectName "api" -Configuration "release" -Runtime "windows"
.\build.ps1 -ProjectName "sso" -Configuration "staging" -Runtime "linux"
```

---

## 💻 Frontend – Angular aplikacija

### 1. Instalacija zavisnosti

```bash
cd frontend
npm install
```

### 2. Pokretanje aplikacije

```bash
npm start
# ili
npm run dev
```

### 3. Konfiguracija API URL-a

U `src/environments/environment.ts`:

```ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7100/api'
};
```

---

## 🔐 Test login podaci

- Da li je aktivan 2FA za korisnike iz seed podataka
- Da li se lozinke generišu automatski (random) ili statično.
  Ako su statične, onda treba ih navesti.

| Uloga     | Email           | Lozinka     |
|-----------|------------------|--------------|
| Admin     | admin@test.com  | Admin123!    |
| Korisnik  | user@test.com   | User123!     |

---

## 📄 Autor

Ime i prezime  
Godina studija  
Predmet: ________