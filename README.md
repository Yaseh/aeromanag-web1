# AeroManag Web

Application de gestion aéronautique (aéroports, avions, personnels, passagers, vols, réservations).

Projet académique réalisé en **Clean Architecture** :
- **Backend** : ASP.NET Core 10 + Dapper + SQLite
- **Frontend** : Angular (standalone components, Signals, Reactive Forms)

---

## Prérequis — ce qu'il faut installer

### 1. .NET SDK 10

Télécharger sur : https://dotnet.microsoft.com/download

Vérifier l'installation :
```bash
dotnet --version
# doit afficher 10.x.x
```

### 2. Node.js (v18 minimum, v22 recommandé)

Télécharger sur : https://nodejs.org

Vérifier :
```bash
node --version   # ex: v22.x.x
npm --version    # ex: 10.x.x
```

### 3. Angular CLI

```bash
npm install -g @angular/cli
```

Vérifier :
```bash
ng version
# doit afficher Angular CLI 17 ou supérieur
```

### 4. SQLite

**Aucune installation requise.** La base de données `aeromanag.db` est créée automatiquement au premier lancement du backend.

---

## Lancement du projet

### Option recommandée — script de lancement unique (un seul terminal)

**Windows (PowerShell) :**

```powershell
.\start-projet.ps1
```

**macOS / Linux :**

```bash
chmod +x start-projet.sh
./start-projet.sh
```

Le script :
1. Démarre le backend en arrière-plan
2. Attend que l'API réponde sur `http://localhost:5176`
3. Affiche les URLs utiles, puis lance le frontend au premier plan
4. **Ctrl+C** arrête proprement les deux processus

URLs après démarrage :
- API (Swagger) : `http://localhost:5176/swagger`
- Frontend : `http://localhost:4200`

Au **premier lancement**, la base `aeromanag.db` est créée automatiquement avec le schéma et les données de démonstration.

---

### Option alternative — deux terminaux séparés

<details>
<summary>Méthode manuelle (si le script ne fonctionne pas)</summary>

**Terminal 1 — Backend (API)**

```bash
cd src/AeroManag.Api
dotnet restore
dotnet run
```

Au démarrage, la console affiche :

```
Now listening on: http://localhost:5176
Application started.
```

**Terminal 2 — Frontend (Angular)**

```bash
cd aeromanag-frontend
npm install
ng serve
```

Puis ouvrir : `http://localhost:4200`

</details>

---

## Structure du projet

```
Aero-manageweb/
│
├── AeroManag.slnx                   ← fichier solution Visual Studio
├── README.md
├── .gitignore
├── start-projet.ps1                 ← script de lancement unique (Windows)
├── start-projet.sh                  ← script de lancement unique (macOS/Linux)
│
├── src/
│   ├── AeroManag.Core/              ← couche métier (aucune dépendance externe)
│   │   ├── Entities/                   6 entités POCO
│   │   ├── DTOs/                       DTOs lecture + écriture
│   │   ├── Interfaces/                 IXxxRepository + IXxxService
│   │   ├── Services/                   logique métier
│   │   └── Exceptions/                 SiegeIndisponibleException
│   │
│   ├── AeroManag.Infrastructure/    ← accès aux données (dépend de Core)
│   │   ├── Data/                       SqliteConnectionFactory
│   │   └── Repositories/              6 repositories Dapper
│   │
│   └── AeroManag.Api/               ← API REST (dépend de Core + Infrastructure)
│       ├── Controllers/               6 controllers
│       ├── Program.cs
│       ├── appsettings.json
│       └── database.sql              schéma + données de démo
│
└── aeromanag-frontend/              ← application Angular standalone
    └── src/app/
        ├── core/api-config.ts        URL de l'API
        ├── models/                   6 interfaces TypeScript
        ├── services/                 6 services (Signals + HttpClient)
        └── components/              12 composants (list + form × 6 entités)
```

---

## Fonctionnalités

| Section | Liste | Créer | Modifier | Supprimer |
|---------|-------|-------|----------|-----------|
| Aéroports | ✓ | ✓ | ✓ | ✓ |
| Avions | ✓ | ✓ | ✓ | ✓ |
| Personnels | ✓ | ✓ | ✓ | ✓ |
| Passagers | ✓ | ✓ | ✓ | ✓ |
| Vols | ✓ | ✓ | ✓ | ✓ |
| Réservations | ✓ | ✓ | — | ✓ |

**Règle métier** : une réservation sur un siège déjà pris retourne une erreur 409 avec le message d'explication affiché dans le formulaire.

---

## API REST — endpoints disponibles

| Méthode | URL | Description |
|---------|-----|-------------|
| GET | `/api/aeroports` | Liste tous les aéroports |
| GET | `/api/aeroports/{id}` | Détail d'un aéroport |
| POST | `/api/aeroports` | Créer un aéroport |
| PUT | `/api/aeroports/{id}` | Modifier un aéroport |
| DELETE | `/api/aeroports/{id}` | Supprimer un aéroport |
| GET | `/api/avions` | Liste tous les avions |
| GET | `/api/avions/{id}` | Détail d'un avion |
| POST | `/api/avions` | Créer un avion |
| PUT | `/api/avions/{id}` | Modifier un avion |
| DELETE | `/api/avions/{id}` | Supprimer un avion |
| GET | `/api/personnels` | Liste tous les personnels |
| GET | `/api/personnels/{id}` | Détail d'un personnel |
| POST | `/api/personnels` | Créer un personnel |
| PUT | `/api/personnels/{id}` | Modifier un personnel |
| DELETE | `/api/personnels/{id}` | Supprimer un personnel |
| GET | `/api/passagers` | Liste tous les passagers |
| GET | `/api/passagers/{id}` | Détail d'un passager |
| POST | `/api/passagers` | Créer un passager |
| PUT | `/api/passagers/{id}` | Modifier un passager |
| DELETE | `/api/passagers/{id}` | Supprimer un passager |
| GET | `/api/vols` | Liste tous les vols (avec jointures) |
| GET | `/api/vols/{id}` | Détail d'un vol |
| POST | `/api/vols` | Créer un vol |
| PUT | `/api/vols/{id}` | Modifier un vol |
| DELETE | `/api/vols/{id}` | Supprimer un vol |
| GET | `/api/reservations` | Liste toutes les réservations |
| GET | `/api/reservations/{id}` | Détail d'une réservation |
| POST | `/api/reservations` | Créer une réservation (409 si siège pris) |
| DELETE | `/api/reservations/{id}` | Supprimer une réservation |

---

## Schéma de la base de données

```
aeroports (id_iata PK, nom, ville, pays)
    |
    |──< vols.aeroport_depart
    |──< vols.aeroport_arrivee

avions (id_avion PK, modele, capacite, statut)
    |──< vols.id_avion

personnels (id_personnel PK, nom, prenom, role)
    |──< vols.id_commandant

vols (id_vol PK, numero_vol, date_depart, date_arrivee, statut, ...)
    |──< reservations.id_vol

passagers (id_passager PK, nom, prenom, nationalite)
    |──< reservations.id_passager

reservations (id_reservation PK, numero_siege, id_vol FK, id_passager FK)
    UNIQUE (id_vol, numero_siege)
```

---

## Architecture technique

### Principe Clean Architecture

```
[ Angular ] ──HTTP──> [ Controllers ]
                           |
                      [ Services ]      ← Core (interfaces + logique)
                           |
                     [ Repositories ]   ← Infrastructure (Dapper + SQLite)
                           |
                       [ SQLite DB ]
```

**Règle de dépendance stricte :**
- `Core` → aucune dépendance externe (pas de Dapper, pas d'ASP.NET)
- `Infrastructure` → dépend de `Core` uniquement
- `Api` → dépend de `Core` et `Infrastructure`

### Choix techniques Angular

| Contrainte | Choix retenu |
|-----------|--------------|
| État global | `signal<T>()` (pas NgRx) |
| Injection | `inject()` (pas de constructeur) |
| Formulaires | `ReactiveFormsModule` + `FormBuilder` |
| Syntaxe template | `@for` / `@if` (Angular 17+) |
| Composants | `standalone: true` |

---

## Données de démonstration incluses

Au premier lancement, ces données sont insérées automatiquement :

**Aéroports** : CDG (Paris), JFK (New York), BRU (Bruxelles)

**Avions** : Airbus A320 (180 places), Boeing 777 (350 places)

**Personnels** : Marie Dupont (Commandant), Jean Lefevre (Commandant)

**Vols** :
- AM101 : CDG → JFK le 01/07/2026
- AM202 : BRU → CDG le 02/07/2026

**Passagers** : Sophie Martin, Carlos Garcia

**Réservations** : sièges 12A et 14C sur AM101, siège 05B sur AM202

---

## Problèmes fréquents

**Le backend ne démarre pas**
- Vérifier que .NET 10 est installé : `dotnet --version`
- Avec le script : relancer `.\start-projet.ps1` depuis la racine du projet
- En méthode manuelle : s'assurer d'être dans `src/AeroManag.Api` avant `dotnet run`

**Le frontend n'affiche pas les données**
- Vérifier que le backend tourne bien (Swagger accessible sur `http://localhost:5176/swagger`)
- Ouvrir la console du navigateur (F12) pour voir les erreurs réseau

**Erreur CORS**
- S'assurer que le frontend est sur `http://localhost:4200`
- Le backend autorise uniquement cette origine

**Réinitialiser la base de données**
- Supprimer le fichier `src/AeroManag.Api/aeromanag.db`
- Relancer `dotnet run` — la base est recréée avec les données de démo
#   w e b - a e r o m a n a g  
 