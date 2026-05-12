CREATE DATABASE scrab2j_lvln
USE scrab2j_lvln;
CREATE TABLE partie
(
    id_user INT,
    prenom_user VARCHAR(50),
    scores_users INT,
    date_partie DATE,
    CONSTRAINT pk_user PRIMARY KEY (id_user)
);
/*-----Création user----------------------------------------------------------------------*/
CREATE user 2jlvln IDENTIFIED BY 'Soleil@Lune';
GRANT ALL ON scrab2j_lvln TO '2jlvln';
