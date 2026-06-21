PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS aeroports (
    id_iata TEXT PRIMARY KEY,
    nom     TEXT NOT NULL,
    ville   TEXT NOT NULL,
    pays    TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS avions (
    id_avion  INTEGER PRIMARY KEY AUTOINCREMENT,
    modele    TEXT NOT NULL,
    capacite  INTEGER NOT NULL,
    statut    TEXT NOT NULL DEFAULT 'En service'
);

CREATE TABLE IF NOT EXISTS personnels (
    id_personnel INTEGER PRIMARY KEY AUTOINCREMENT,
    nom          TEXT NOT NULL,
    prenom       TEXT NOT NULL,
    role         TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS vols (
    id_vol           INTEGER PRIMARY KEY AUTOINCREMENT,
    numero_vol       TEXT NOT NULL,
    date_depart      TEXT NOT NULL,
    date_arrivee     TEXT NOT NULL,
    statut           TEXT NOT NULL DEFAULT 'Prevu',
    aeroport_depart  TEXT NOT NULL,
    aeroport_arrivee TEXT NOT NULL,
    id_avion         INTEGER NOT NULL,
    id_commandant    INTEGER NOT NULL,
    FOREIGN KEY (aeroport_depart)  REFERENCES aeroports(id_iata) ON DELETE RESTRICT,
    FOREIGN KEY (aeroport_arrivee) REFERENCES aeroports(id_iata) ON DELETE RESTRICT,
    FOREIGN KEY (id_avion)         REFERENCES avions(id_avion)   ON DELETE RESTRICT,
    FOREIGN KEY (id_commandant)    REFERENCES personnels(id_personnel) ON DELETE RESTRICT
);

CREATE TABLE IF NOT EXISTS passagers (
    id_passager  INTEGER PRIMARY KEY AUTOINCREMENT,
    nom          TEXT NOT NULL,
    prenom       TEXT NOT NULL,
    nationalite  TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS reservations (
    id_reservation INTEGER PRIMARY KEY AUTOINCREMENT,
    numero_siege   TEXT NOT NULL,
    id_vol         INTEGER NOT NULL,
    id_passager    INTEGER NOT NULL,
    FOREIGN KEY (id_vol)      REFERENCES vols(id_vol)           ON DELETE CASCADE,
    FOREIGN KEY (id_passager) REFERENCES passagers(id_passager) ON DELETE CASCADE,
    UNIQUE (id_vol, numero_siege)
);

INSERT INTO aeroports (id_iata, nom, ville, pays) VALUES
('CDG', 'Aeroport Charles de Gaulle', 'Paris', 'France'),
('JFK', 'John F. Kennedy International', 'New York', 'Etats-Unis'),
('BRU', 'Aeroport de Bruxelles-National', 'Bruxelles', 'Belgique');

INSERT INTO avions (modele, capacite, statut) VALUES
('Airbus A320', 180, 'En service'),
('Boeing 777', 350, 'En service');

INSERT INTO personnels (nom, prenom, role) VALUES
('Dupont', 'Marie', 'Commandant'),
('Lefevre', 'Jean', 'Commandant');

INSERT INTO vols (numero_vol, date_depart, date_arrivee, statut, aeroport_depart, aeroport_arrivee, id_avion, id_commandant) VALUES
('AM101', '2026-07-01T08:00', '2026-07-01T10:30', 'Prevu', 'CDG', 'JFK', 1, 1),
('AM202', '2026-07-02T14:00', '2026-07-02T15:15', 'Prevu', 'BRU', 'CDG', 2, 2);

INSERT INTO passagers (nom, prenom, nationalite) VALUES
('Martin', 'Sophie', 'Belge'),
('Garcia', 'Carlos', 'Espagnole');

INSERT INTO reservations (numero_siege, id_vol, id_passager) VALUES
('12A', 1, 1),
('14C', 1, 2),
('05B', 2, 1)
