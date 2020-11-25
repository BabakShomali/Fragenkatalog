#--------------------------------------------------------------------------------------------------------------------------------------------
# fragemkatalog-Datenbank wird gelöscht.
# Bereits vorhandene Daten gehen verloren!
#--------------------------------------------------------------------------------------------------------------------------------------------
DROP DATABASE IF EXISTS `Fragenkatalog`;
CREATE DATABASE `Fragenkatalog`;

#--------------------------------------------------------------------------------------------------------------------------------------------
# Rolle
#--------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE `Fragenkatalog`.`T_Rolle` 
( 
	`p_rolle_nr` INT UNSIGNED NOT NULL AUTO_INCREMENT , 
	`name`       VARCHAR(30) NOT NULL , 
	PRIMARY KEY (`p_rolle_nr`), 
	UNIQUE `rolle_name` (`name`)
) ENGINE = InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

INSERT INTO `Fragenkatalog`.`T_Rolle` (`p_rolle_nr`, `name`) VALUES (NULL, 'Admin'), (NULL, 'Dozent'), (Null, 'Schueler');

#--------------------------------------------------------------------------------------------------------------------------------------------
# Benutzer
#--------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE `Fragenkatalog`.`T_Benutzer` 
( 
	`p_benutzer_nr` INT UNSIGNED NOT NULL AUTO_INCREMENT, 
	`login_name`    VARCHAR(30) NOT NULL, 
	`email_adresse` VARCHAR(30) NOT NULL,
	`passwort`		VARCHAR(41) NOT NULL,
	PRIMARY KEY (`p_benutzer_nr`), 
	UNIQUE `benutzer_login_name` (`login_name`),
	UNIQUE `benutzer_email_adresse` (`email_adresse`)
) ENGINE = InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

ALTER TABLE `Fragenkatalog`.`T_Benutzer` ADD COLUMN fk_rolle_nr INT UNSIGNED NOT NULL;

ALTER TABLE `Fragenkatalog`.`T_Benutzer` ADD CONSTRAINT cfk_rolle_nr 
	FOREIGN KEY(`fk_rolle_nr`) REFERENCES `Fragenkatalog`.`T_Rolle`(`p_rolle_nr`)
	ON UPDATE CASCADE;	
	
CREATE TABLE `Fragenkatalog`.`T_Admins`
(
	`p_f_benutzer_nr` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`p_f_benutzer_nr`)
) ENGINE = InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

ALTER TABLE `Fragenkatalog`.`T_Admins` ADD CONSTRAINT cfk_admins_benutzer_nr
	FOREIGN KEY(`p_f_benutzer_nr`) REFERENCES `Fragenkatalog`.`T_Benutzer`(`p_benutzer_nr`)
	ON UPDATE CASCADE;
	
	
CREATE TABLE `Fragenkatalog`.`T_Dozenten`
(
	`p_f_benutzer_nr` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`p_f_benutzer_nr`)
) ENGINE = InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

ALTER TABLE `Fragenkatalog`.`T_Dozenten` ADD CONSTRAINT cfk_dozenten_benutzer_nr
	FOREIGN KEY(`p_f_benutzer_nr`) REFERENCES `Fragenkatalog`.`T_Benutzer`(`p_benutzer_nr`)
	ON UPDATE CASCADE;

	
CREATE TABLE `Fragenkatalog`.`T_Schueler`
(
	`p_f_benutzer_nr` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`p_f_benutzer_nr`)
) ENGINE = InnoDB CHARSET=utf8 COLLATE utf8_general_ci;

ALTER TABLE `Fragenkatalog`.`T_Schueler` ADD CONSTRAINT cfk_schueler_benutzer_nr
	FOREIGN KEY(`p_f_benutzer_nr`) REFERENCES `Fragenkatalog`.`T_Benutzer`(`p_benutzer_nr`)
	ON UPDATE CASCADE;

#--------------------------------------------------------------------------------------------------------------------------------------------
# MySQL-User definieren Zugriffsrechte für Rollen
#--------------------------------------------------------------------------------------------------------------------------------------------

DROP USER IF EXISTS `fragenkatalog_admin`;
CREATE USER `fragenkatalog_admin` IDENTIFIED BY 'LKasdZ%&!?*';
GRANT SELECT, UPDATE, DELETE, INSERT ON Fragenkatalog.* TO fragenkatalog_admin;

DROP USER IF EXISTS `fragenkatalog_dozent`;
CREATE USER `fragenkatalog_dozent` IDENTIFIED BY 'GJKa1PdZ%&asw';
GRANT SELECT ON Fragenkatalog.* TO fragenkatalog_dozent;

DROP USER IF EXISTS `fragenkatalog_schueler`;
CREATE USER `fragenkatalog_schueler` IDENTIFIED BY 'SdfgrP12&qwf3a';
GRANT SELECT ON Fragenkatalog.* TO fragenkatalog_schueler;

DROP USER IF EXISTS `fragenkatalog_benutzer`;
CREATE USER `fragenkatalog_benutzer` IDENTIFIED BY 'UdBadrPwqr23w$*a';
GRANT SELECT ON Fragenkatalog.* TO fragenkatalog_benutzer;

#--------------------------------------------------------------------------------------------------------------------------------------------
# Test-Benutzer
#--------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Fragenkatalog.T_Benutzer (`login_name`,`email_adresse`, `passwort`, `fk_rolle_nr`)
							  VALUES ('Primus', 'primus@bbq.de', PASSWORD('primus123'), 1);
INSERT INTO Fragenkatalog.T_Admins (`p_f_benutzer_nr`) SELECT `p_benutzer_nr` FROM Fragenkatalog.T_Benutzer WHERE `login_name` = 'Primus';

INSERT INTO Fragenkatalog.T_Benutzer (`login_name`,`email_adresse`, `passwort`, `fk_rolle_nr`)
							  VALUES ('Dozi', 'dozi@bbq.de', PASSWORD('dozi123'), 2);
INSERT INTO Fragenkatalog.T_Dozenten (`p_f_benutzer_nr`) SELECT `p_benutzer_nr` FROM Fragenkatalog.T_Benutzer WHERE `login_name` = 'Dozi';

INSERT INTO Fragenkatalog.T_Benutzer (`login_name`,`email_adresse`, `passwort`, `fk_rolle_nr`)
							  VALUES ('Schuli', 'schuli@bbq.de', PASSWORD('sch123'), 3);
INSERT INTO Fragenkatalog.T_Schueler (`p_f_benutzer_nr`) SELECT `p_benutzer_nr` FROM Fragenkatalog.T_Benutzer WHERE `login_name` = 'Schuli';


#--------------------------------------------------------------------------------------------------------------------------------------------
# Frage
#--------------------------------------------------------------------------------------------------------------------------------------------


DROP TABLE IF EXISTS `Fragenkatalog`.`T_Fragen`;
CREATE TABLE IF NOT EXISTS `Fragenkatalog`.`T_Fragen` (
  `p_frage_nr` int(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `frage` varchar(500) NOT NULL,
  `antwortA` varchar(500) NOT NULL,
  `antwortB` varchar(500) NOT NULL,
  `antwortC` varchar(500) NOT NULL,
  `antwortD` varchar(500) NOT NULL,
  `loesung` char(1) NOT NULL,
  PRIMARY KEY (`p_frage_nr`)
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=latin1;

INSERT INTO `Fragenkatalog`.`T_Fragen` (`p_frage_nr`, `frage`, `antwortA`, `antwortB`, `antwortC`, `antwortD`, `loesung`) VALUES
(1, 'Welche medizinischen Voraussetzungen sollte der Taucher für diesen Sport mitbringen?\r\n', 'Ärztliches Attest über Tauchtauglichkeit.', 'Ärztliche Tauchtauglichkeit für Berufstaucher.', 'Selbstauskunft über die Gesundheit.\r\n', 'Man braucht keine tauchärztliche Untersuchung.', 'a'),
(2, 'Zur Teilnahme am Tauchsport muss man: …?\r\n', 'Gesund sein.', 'Körperlich und geistig normal belastbar sein.\r\n', 'Schwimmen können.\r\n', 'Alles oben genannte.\r\n', 'd'),
(3, 'Was versteht man beim Tauchen unter „ABC-Ausrüstung“?', 'Badehose, Maske und Schnorchel.', 'Maske, Schnorchel und Flossen.', 'Maske, Schnorchel und Shorty.', 'Shorty, Maske und Flossen.', 'b'),
(4, 'Wie muss eine Maske beschaffen sein?', 'Zwei Gläser, Maskenkörper und Kopfband.', 'Gummikörper, Nasenerker und bruchfestes Glas.', 'Gummi- oder Silikonmaskenkörper, Nasenerker für den Druckausgleich, bruchfestes Sicherheitsglas, verstellbares\r\nKopfband aus Gummi oder Silikon.', 'Es kann auch eine Schwimmbrille zum Tauchen benutzt werden.', 'c'),
(5, 'Vor Gebrauch der Tauchmaske sind folgende Schritte wichtig.\r\n', 'Die Maske ist sofort einsatzbereit.', 'Das Maskenband muss immer neu eingestellt werden.', 'Der Maskenkörper muss mit Talkum eingerieben werden.', 'Die Maskengläser müssen vor dem ersten Gebrauch mit einem speziellen Antibeschlagmittel gereinigt werden, damit\r\ndie Maske beim Tauchen nicht beschlagen kann.', 'd'),
(6, 'Damit die Tauchmaske beim Tauchen nicht ständig mit Wasser vollläuft, sind vor dem ersten Gebrauch\r\nfolgende Schritte wichtig.', 'Beim Kauf darauf achten, dass die Maske der eigenen Hutgröße entspricht.', 'Dass die Maske in jedem Fall mit optischen Gläsern versehen werden kann.', 'Die Maske muss auch meinem Tauchpartner passen.', 'Die Maske muss auch ohne Maskenband auf dem Gesicht haften bleiben.', 'd'),
(7, 'Wie wird die Maske gepflegt und transportiert?', 'Masken aus Silikon müssen nicht gepflegt werden.\r\n', 'Nach dem Tauchen mit Süßwasser spülen, im Schatten trocknen lassen und danach in der vorgesehene Maskenbox\r\nverstauen.', 'Nach dem Tauchen die Maske in der Sonne trocknen lassen mit Süßwasser spülen und nach dem Trocknen mit\r\nSilikonspray pflegen.', 'Alle Antworten sind richtig.\r\n', 'b'),
(8, 'Wie muss ein Schnorchel beschaffen sein?', 'Maximale Länge 35 cm bei Erwachsenen und max. 30 cm bei Kindern. Passendes Mundstück mit oder ohne\r\nAusblasventil.', 'Möglichst langer Schnorchel aus Kunststoff, damit man möglichst tief tauchen kann.', 'Möglichst großer Durchmesser, damit man bei Anstrengung viel Luft bekommt.', 'b) und c) sind richtig.\r\n', 'a'),
(9, 'Wie sollte eine Tauchflosse beschaffen sein?', 'Eine Flosse sollte möglichst lang sein, damit man schnell vorwärts kommt.', 'Eine Flosse sollte möglichst hart sein, damit man schnell vorwärts kommt.', 'a) und b) sind richtig.', 'Eine Flosse sollte gut sitzen und dem Trainingszustand des Tauchers angepasst sein.\r\n', 'd'),
(10, 'Warum gibt es offene und geschlossene Flossen?', 'Geschlossene Flossen sind besser für die Wärmedämmung des Fußes als offene Flossen.', 'Schuhflossen kann man ohne Füßlinge tragen. Sie werden überwiegend zum Schnorcheln und im Schwimmbad\r\nbenutzt. Fersenbandflossen sind offen und werden mit Füßlingen getragen und mit einem Fersenband fixiert. Durch\r\ndie Füßlinge besteht eine gute Wärmeisolierung. Sie werden hauptsächlich zum Freiwassertauchen benutzt.', 'Offene oder auch Geräteflossen sind nur für das Tauchen mit einem Trockentauchanzug geeignet. ', 'Schuh- und Fersenbandflossen sind immer gleich lang.\r\n', 'b'),
(11, 'Warum ist ein Neopren-Kälteschutz beim Tauchen sehr wichtig?', 'Zum Schutz vor Kälte und Verletzungen.', 'Damit man länger als eine Stunde tauchen kann.', 'Damit die Ausrüstung nicht auf der Haut scheuert.', 'Damit kein Barotrauma der Haut entstehen kann.\r\n', 'a'),
(12, 'Wodurch unterscheiden sich Nass-, Halbtrocken- und Trockentauchanzüge?', 'Nur durch unterschiedliche Stärken.', 'Für Halbtrockenanzüge braucht man eine Spezialausbildung.', 'Bis auf den Preis gibt es keinen Unterschied.', 'Durch unterschiedlich großen Wasseraustausch – je weniger Wasseraustausch umso wärmer.', 'd'),
(13, 'Welchen Zweck erfüllen autonome Leichttauchgeräte (DTG/ SCUBA bestehend aus Flasche, Ventil,\r\nAtemregler)?', 'Sie sind nur für das Helmtauchen gedacht.', 'Autonome Leichttauchgeräte sind nur für das Tauchen mit Nitrox geeignet und bestehen immer aus Aluminium.', 'Autonome Leichttauchgeräte versorgen den Taucher mit lebenswichtigen Atemgasen wie Luft oder Nitrox. Die\r\nFlasche des DTG‘s besteht aus Aluminium oder Stahl.', 'Autonome Leichttauchgeräte sind ausschließlich für Feuerwehrtaucher gedacht.\r\n', 'c'),
(14, 'Ein Atemregler lässt sich nicht von der Flasche abschrauben. Was kann die Ursache sein?', 'Das Flaschenventil und der Atemregler sind nicht kompatibel.', 'Das Flaschenventil wurde geschlossen, aber die Luftdusche der 2. Stufe des Atemreglers nicht gedrückt, um den\r\nRestdruck entweichen zu lassen.', 'Das Flaschenventil wurde linksherum zugedreht.', 'Das Ventil sitzt mit Rost aus der Flasche zu.', 'b'),
(15, 'Worauf ist beim Tauchen in Gewässern unter 10 Grad Celsius zu achten?', 'Die EN 250 schreibt vor, dass zwei unabhängige und separat absperrbare Atemregler und zwei Warneinrichtungen\r\nvorhanden sein müssen.', 'Die Atemregler müssen einen Eisschutz haben.\r\n', 'Die Atemregler müssen vor dem Tauchen warm gelagert werden.', 'Die Atemregler unterliegen einem besonderen Service bei einem Fachhändler.', 'a'),
(16, 'Warum tauchen wir immer mit einem Jacket?', 'Damit wir an der Wasseroberfläche schwimmen können.', 'Damit wir den Abtrieb beim Tauchen kompensieren können.', 'Damit wir die Sicherheit beim Tauchen erhöhen, indem wir den Abtrieb kompensieren und an der Wasseroberfläche\r\neinfacher schwimmen können.', 'Das Jacket dient einzig und allein zur Rettung des Tauchpartners.', 'c'),
(17, 'Was ist beim Tauchen in Bezug auf das Gewichtsystem (Blei) zu berücksichtigen?', 'Der Taucher muss 10 % seines Körpergewichtes an Bleimenge mitnehmen, damit er abtauchen kann.', 'Der Taucher muss so viel Blei mitnehmen, dass er auf dem Sicherheitsstopp und der verbliebenen Reserveluft und mit\r\nleerem Jacket noch im hydrostatischen Gleichgewicht ist.', 'Der Taucher muss so viel Blei mitnehmen, dass er auf dem Sicherheitsstopp mit leerem Gerät und leerem Jacket noch\r\nim hydrostatischen Gleichgewicht ist.', 'Das Gewichtsystem muss so fixiert sein, dass es nicht verrutschen kann.\r\n', 'b'),
(18, 'Um den Tauchgang sicher zu planen und durchführen zu können, muss der Taucher mindestens Folgendes\r\nmit sich führen:', 'Einen Tauchcomputer (oder Uhr, Tiefenmesser, Tauchtabelle) und ein Finimeter.\r\n', 'Einen Tiefenmesser und eine Austauchabelle.\r\n', 'Nur einen Tauchcomputer.', 'Einen Tauchcomputer und ein Finimeter.', 'a'),
(19, 'Wie orientiert sich ein Taucher UW?', 'Nach der Sonne und dem Tiefenmesser.', 'Mit der natürlichen Orientierung und dem Kompass.', 'Mit der Seekarte.', 'Ich verlasse mich auf den Gruppenführer.\r\n', 'b'),
(20, 'Warum führt ein Taucher ein geeignetes Messer/Schere mit sich?', 'Um sich eventuell aus Fischernetzen, Angelschnüren, Seilen etc. zu befreien und um es als Werkzeug zu benutzen.', 'Als Waffe gegen Haie.\r\n', 'Um im Notfall die Luft aus dem Jacket zu bekommen.\r\n', 'Um Vernesselungen von der Haut zu schaben.\r\n', 'a'),
(21, 'Wozu benutzt der Taucher eine Lampe?\r\n', 'Um Wracks zu finden.\r\n', 'Beim Nachttauchen und um die verloren gegangenen Farben UW wieder zu sehen.\r\n', 'Um UW SOS zu erzeugen.\r\n', 'Um im Dunkeln seine Tauchausrüstung zusammenbauen zu können.\r\n', 'b'),
(22, 'Was verstehst du unter dem Wasser-Nase-Reflex?', 'Die Nase schließt sich automatisch, wenn sie mit Wasser in Verbindung kommt.\r\n', 'Beim Wasser-Nase-Reflex wird unwillkürlich ausgeatmet, wenn Wasser in die Nase kommt. ', 'Wenn Wasser in die Nase kommt, kommt es automatisch zu reflektorischem Ausatmen.\r\n', ' Beim Wasser-Nase-Reflex wird die Atmung unwillkürlich unterbrochen; als Schutz vor versehentlichem Aspirieren\r\nvon Wasser.\r\n', 'd'),
(23, 'Was versteht der Taucher unter Druckausgleich?', 'Das Atmen aus dem autonomen Leichttauchgerät unter Wasser.', 'Den höheren Druck UW durch verschiedene Methoden auszugleichen. Zum Beispiel beim tiefer Tauchen durch die\r\nNase auszuatmen, oder das Zuhalten der Nase mit gleichzeitigem Ausatmen gegen die Nase, um das Trommelfell\r\nwieder gerade zu stellen.\r\n', 'Beim Absinken Luft ins Jacket geben, um wieder zu schweben.\r\n', 'Beim Auftauchen permanent abatmen, damit die Lunge nicht platzt.\r\n', 'b'),
(24, 'Welche Antwort ist falsch?\r\n', 'Nach jedem Tauchgang Ausrüstung in Süßwasser spülen.\r\n', 'Ausrüstung nach dem Trocknen in trockenem abgedunkeltem Raum lagern.\r\n', 'Atemregler mit verschlossener 2. Stufe und ohne an der 2. Stufe die Luftdusche zu drücken in Süßwasser spülen.\r\n', 'Gummiteile und Manschetten mit Silikonspray pflegen.\r\n', 'd'),
(25, 'Wie atmet man UW richtig?', 'Tief einatmen und die Luft anhalten, damit man Luft spart.', 'Die Atmung unter Wasser soll sich nicht wesentlich von der Atmung über Wasser unterscheiden.\r\n', 'Tief ausatmen, damit man weniger Blei braucht.\r\n', 'Ganz flach atmen, damit man weniger mit Stickstoff gesättigt wird.\r\n', 'b'),
(26, 'Wann schwimmt ein Körper an der Wasseroberfläche?\r\n', 'Reines Wasser hat eine Dichte von 1 kg/l. Hat der ins Wasser eintauchende Körper weniger als 1 kg/l, dann ist er\r\nleichter als Wasser und schwimmt.\r\n', 'Wenn sein Volumen sehr groß ist.\r\n', 'Bei sehr warmem Wasser.\r\n', 'Wenn der Salzgehalt sehr niedrig ist.', 'a'),
(27, 'Wie viel Druck herrscht in Meereshöhe in 15 m Tiefe?', '1,5 bar', '2,5 bar', '3 bar', '3,5 bar', 'b'),
(28, 'Wie viel Druck herrscht in Meereshöhe in 25 m Tiefe?\r\n', '1 bar', '2 bar', '2,5 bar', '3,5 bar', 'd'),
(29, 'In welcher Tiefe ist die relative Druckzunahme am größten?\r\n', '0 bis 5m', '0 bis 10 m', '10 bis 20 m', '20 bis 30 m\r\n', 'b'),
(30, 'Wenn ein aufgeblasener und verschlossener Luftballon aus 30 m Tiefe aufsteigt, in welchem Tiefenbereich\r\nnimmt das Volumen am meisten zu?\r\n', '30 m bis 20 m', '20 m bis 30 m\r\n', '15 m bis 10 m', '10 m bis 0 m\r\n', 'd'),
(31, 'Wenn Du beim Abtauchen plötzlich keinen Druckausgleich mehr bekommst, was machst Du?', 'Kräftig Luft aus der Lunge gegen die zugehaltene Nase drücken.\r\n', 'Kräftig schlucken und dann gegen die Nase drücken.\r\n', 'Etwas höher tauchen und nochmals leicht den Druckausgleich versuchen.\r\n', 'Auftauchen und den Tauchgang beenden.\r\n', 'c'),
(32, 'Wenn Du erkältet bist und tauchen möchtest, was machst Du?', 'Nehme Nasentropfen und kann dann problemlos tauchen.\r\n', 'Nehme Nasentropfen und eine Aspirin und kann dann problemlos tauchen.\r\n', 'Gehe erst wieder tauchen, wenn die Erkältung abgeklungen ist.\r\n', 'Gehe sofort zum Arzt und lass mir ein entsprechendes Medikament verschreiben, um tauchen gehen zu können.\r\n', 'c'),
(33, 'Was ist ein Überdruckbarotrauma?\r\n', 'Eine Erkrankung durch zu hohen Druck, der nicht schnell genug ausgeglichen werden konnte.\r\n', 'Eine Erkrankung durch zu hohen Luftdruck.\r\n', 'Eine Druckzunahme von 100 %.\r\n', 'Wenn der Taucher zu lange tauchen war.\r\n', 'a'),
(34, 'Ein Unterdruckbarotrauma …', 'Entsteht erst in Tiefen ab 20 m.', 'Entsteht, wenn dem Körper schnell Luft entzogen wird.', 'Entsteht, wenn der entstehende Unterdruck nicht schnell genug ausgeglichen wird.\r\n', 'Kann nur in einer Tauchermaske vorkommen.\r\n', 'c'),
(35, 'Welche Aufgabe haben die Atmung und das Herz-Kreislauf-System?', 'Zwischen beiden besteht kein Zusammenhang.', 'Beide Systeme sind unabhängig voneinander.\r\n', 'Die Atmung steuert das Herz-Kreislauf-System.', 'Die Atmung versorgt den Körper mit Sauerstoff und das Herz-Kreislauf-System transportiert den Sauerstoff in die\r\nZellen.', 'd'),
(36, 'Aus welchen Bestandteilen besteht unsere Einatemluft?', '21 % Sauerstoff, 78 % Stickstoff, 0,4 % Kohlendioxyd und 8,6 % Edelgasen', '21 % Sauerstoff, 78 % Stickstoff, 0,03 % Kohlendioxyd und 0,97 % Edelgasen', '21 % Sauerstoff, 78 % Stickstoff, 0,04 % Kohlendioxyd und 0,97 % Edelgasen', '17 % Sauerstoff, 79 % Stickstoff, 0,03 % Kohlendioxyd und 3,97 % Edelgasen', 'b'),
(37, 'Was bewirkt die Hyperventilation?', 'Der Körper wird zu 100 % mit Sauerstoff gesättigt.', 'Der Taucher kann gefahrlos mit angehaltenem Atem weiter Streckentauchen (Apnoetauchen).', 'Durch die Hyperventilation wird der Stickstoff stark abgeatmet.', 'Der Körper baut verstärkt CO2 ab. Es kommt zum Schwimmbad-Blackout.', 'd'),
(38, 'Was ist bei einem entstehenden Essoufflement unter Wasser zu tun?', 'Tauchgang sofort abbrechen, da Lebensgefahr besteht.', 'Bewegung sofort einstellen und dem Tauchpartner das Zeichen für außer Atem geraten geben.', 'Sofort mit der Wechselatmung beginnen.', 'Blei abwerfen und langsamer atmen.', 'b'),
(39, 'Starker Flüssigkeitsverlust vor dem Tauchen kann zu Dehydration führen. Was kann die Ursache sein?', 'Zu fett und zu salzig gegessen.\r\n', 'Zu viel Kaffee getrunken.\r\n', 'Zu wenig getrunken.\r\n', 'Zu oft zur Toilette gegangen.\r\n', 'c'),
(40, 'Was kann zu starkem Flüssigkeitsverlust beim Tauchen führen?\r\n', 'Schwitzen und die trockene Atemluft aus dem autonomen Leichttauchgerät (DTG).\r\n', 'Angst.', 'Der hohe Druck auf den Körper.\r\n', 'Die Anstrengung beim Tauchen.', 'a'),
(41, 'Warum können wir Schallquellen UW kaum orten?', 'Durch den hohen Druck ist unser Gehör stark eingeschränkt.', 'Der dreidimensionale Raum unterscheidet sich zu sehr von den Verhältnissen über Wasser.', 'Die hohe Dichte vom Wasser im Verhältnis zur geringen Dichte der Luft überträgt den Schall unter Wasser so schnell,\r\ndass unsere Ohren den Schall links und rechts gleichzeitig wahrnehmen. Über Wasser trifft der Schall unterschiedlich\r\nauf die Ohren und wir wissen in etwa, woher das Geräusch kommt.', 'Unser Trommelfell ist durch den Wasserdruck so beansprucht, dass es die Schallquelle nicht wahrnehmen kann.\r\nInfolge dessen werden über die Gehörknöchelchen Impulse an die Bogengänge im Innenohr weiter gegeben.', 'c'),
(42, 'Wie sehen wir mit der Tauchermaske UW?', 'Wir sehen alles um 1/3 näher.\r\n', 'Wir sehen alles um 1/3 kürzer.', 'Gegenstände werden in 1/3 der tatsächlichen Größe wahrgenommen.\r\n', 'Gegenstände scheinen 1/3 größer zu sein.\r\n', 'd'),
(43, 'Warum verschwinden mit zunehmender Wassertiefe die Farben immer mehr?\r\n', 'Durch das dichtere Medium Wasser und die zunehmende Tiefe werden die verschiedenen Anteile des Lichts nach und\r\nnach absorbiert. Dadurch verschwinden die Farben immer mehr.', ' Je mehr Schwebeteilchen im Wasser sind, desto eher sind die Farben nicht mehr zu sehen.\r\n', 'Die Farben verschwinden durch die Veränderung unseres Sehnervs mit Zunahme des Drucks immer mehr.\r\n', 'Durch die Lichtbrechung der Tauchmaske verschwinden die Farben mit zunehmender Tiefe immer mehr.\r\n', 'a'),
(44, 'Was bedeutet die Lichtstreuung unter Wasser für uns Taucher?', 'Wenn die Sonne verschwindet, wird die Streuung weniger und wir können viel weiter sehen.', 'Die Lichtstreuung hat nur Einfluss auf unsere Sicht, wenn wir über hellem Untergrund tauchen.', 'Das in Wasser einfallende Licht wird durch feinste im Wasser befindliche Teilchen gestreut.', 'Da Wasser viel dichter ist als Luft, hat die Lichtstreuung keine Bedeutung beim Tauchen.', 'c'),
(45, 'Nenne die richtige Reihenfolge der Tauchphasen eines Tauchgangs.', 'Dekompression, Isopression, Kompression', 'Kompression, Dekompression, Isopression\r\n', 'Isopression, Kompression, Dekompression\r\n', 'Kompression, Isopression, Dekompression\r\n', 'd'),
(46, 'In welcher Tauchphase befinden sich Kontrollstopp und Sicherheitsstopp?\r\n', 'Kontrollstopp in der Kompressionsphase und Sicherheitsstopp in der Dekompressionsphase.\r\n', 'Kontrollstopp in der Isopressionsphase und Sicherheitsstopp in der Kompressionsphase.\r\n', 'Kontrollstopp in der Kompressionsphase und Sicherheitsstopp in der Isopressionsphase.\r\n', 'Kontrollstopp in der Dekompressionsphase und Sicherheitsstopp in der Kompressionsphase.', 'a'),
(47, 'Was beschreibt das Gesetz von HENRY beim Tauchen?\r\n', 'Löslichkeit von Alkohol im Blut.', 'Das Gesetz von HENRY beschäftigt sich mit der Löslichkeit von Gasen in Flüssigkeiten.\r\n', 'Das Gesetz von HENRY beschäftigt sich mit der Dichte von Gasen bei erhöhtem Druck.\r\n', 'Das Gesetz von HENRY beschäftigt sich mit dem Druck und der Temperatur.\r\n', 'b'),
(48, 'Was beschreibt einen Dekompressionsunfall?', 'Zu tief und zu lange getaucht.', 'Zu schnelles Auftauchen.\r\n', 'Der Körper kann den gesättigten Stickstoff nicht so schnell abatmen und daher perlt der Stickstoff an irgendeiner\r\nStelle im Körper aus und verursacht Störungen.\r\n', 'Atemnot, hell roter Schaum vor dem Mund.\r\n', 'c'),
(49, 'Tauchgangberechnung nach Tabelle: \r\n1. TG 10 Uhr, 18 m, 30 Minuten\r\n2. TG 14 Uhr, 13 m, 30 Minuten\r\nAMV 20 L/Min.\r\nFrage: Wie viel Liter Luft wurde für bei Tauchgänge insgesamt verbraucht?\r\n', '4.300 L', '3.940 L', '3.830 L\r\n', '3.570 L\r\n', 'd'),
(50, 'Mit welchen Aufstiegsgeschwindigkeiten tauchen wir maximal auf?\r\n', 'Von 40 m – 20 m mit 25 m/ min\r\nVon 20 m – 10 m mit 15 m/min\r\nVon 10 m – 0 m mit 6 m/min', 'Von 40 m – 10 m mit 15 m/ min\r\nVon 10 m – 0 m mit 6 m/min', 'Von 40 m – 10 m mit 10 m/ min\r\nVon 10 m – 0 m mit 6 m/min\r\n', 'Von 40 m – 0 m mit 10m/ min', 'c'),
(51, 'Warum muss ein Briefing vor dem Tauchen gewissenhaft durchgeführt werden?', 'Geplanten Tauchgang nochmals besprechen.', 'Information über den Gruppenführer, den Tauchpartner, die Gesundheit und die Gruppeneinteilung.\r\n', 'Angaben zum Tauchplatz, Tauchtiefe, Tauchzeit, Handzeichen und besondere Absprachen.\r\n', 'Alle Antworten sind richtig.\r\n', 'd'),
(52, 'Wie schütze ich die Umwelt vor dem Tauchen?\r\n', 'Möglichst Fahrgemeinschaften zum Tauchplatz bilden.', 'Genügend Flaschen mitnehmen, damit am Gewässer kein Kompressor benötigt wird.\r\n', 'Anfallenden Müll mit nach Hause nehmen, offene Feuerstellen (Grillen) vermeiden.\r\n', 'Alle Antworten sind richtig.\r\n', 'd'),
(53, 'Wie schone ich die Umwelt beim Tauchen?', 'Während eines Tauchgangs den Müll mit aus dem Wasser nehmen.\r\n', 'Keine Gegenstände aus dem Wasser mitnehmen, die bereits von Tieren als Behausung angenommen wurden oder auf\r\ndenen sich bereits Korallenbewuchs eingestellt hat.\r\n', 'Behörden auf Besonderheiten des Gewässers hinweisen (z.B. Ölverschmutzung, Verunreinigungen durch Dritte).', 'Alle Antworten sind richtig.\r\n', 'd'),
(54, 'Wie können Verletzungen durch Meerestiere vermieden werden?\r\n', 'Durch richtige Tarierung, Abstand zu Meeresbewohnern und eine vorsichtige Verhaltensweise an Riffen, kann die\r\nGefahr unter Wasser minimiert werden.\r\n', 'Verletzungen können nicht auftreten, wenn der Taucher einen kompletten Tauchanzug trägt.\r\n', 'Meerestiere haben Angst und flüchten vor den Tauchern.\r\n', 'Nur Haie können den Tauchern gefährlich werden.\r\n', 'a'),
(55, 'Welche Verletzungen können Meerestiere verursachen?', 'Bissverletzungen mit und ohne Gift.', 'Stich- und Schnittverletzungen mit und ohne Giftbeteiligung.\r\n', 'Vernesselungen.\r\n', 'alle Antworten sind richtig.\r\n', 'd'),
(56, 'Muss ein Taucher ein internationales Tauchbrevet besitzen?\r\n', 'Nein, es gelten die gleichen Bestimmungen wie für Schwimmer.', 'Je nach Tauchgebiet muss er OWD oder AOWDbesitzen, um tauchen zu dürfen.', 'Nein, muss er nicht, wenn er einen Checktauchgang absolviert.\r\n', 'Er muss mindestens Dive Leader/CMAS*** sein, um alleine mit einem gleich starken Partner tauchen zu dürfen.\r\n', 'b'),
(57, 'Wozu dient die Training Record Card in der Ausbildung von Tauchern?', 'Sie ist der Rote Faden für die Ausbildung.\r\n', 'Sie regelt die die Voraussetzungen, um mit der Ausbildung beginnen zu können.\r\n', 'Sie dient als Überweisungsschein, wenn man bei einem Tauchlehrer die Ausbildung nicht beenden konnte.', 'Alle Antworten sind richtig.\r\n', 'd'),
(58, 'Warum sind Dachorganisationen wie die CMAS oder der RSTC für uns Taucher wichtig?\r\n', 'Sie sind irrelevant, da jede Tauchsportorganisation ihre eigenen Standards zum Tauchen haben.\r\n', 'Sie sind ein wichtiger Bestandteil der EUF und sorgen für einheitliche Standards und Äquivalenzen.', 'Es gibt nur die CMAS, die sich um uns Taucher kümmert.\r\n', 'Es gibt nur den RSTC, der sich weltweit um uns Taucher kümmert.\r\n', 'b'),
(59, 'Welcher Sport ist laut Statistik gefährlicher als das Tauchen?\r\n', 'Reiten\r\n', 'Skifahren\r\n', 'Fußball\r\n', 'Alle Antworten sind richtig', 'd');


#--------------------------------------------------------------------------------------------------------------------------------------------
# Fach
#--------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE `Fragenkatalog`.`T_Faecher`
(
	p_fach_nr INT UNSIGNED NOT NULL AUTO_INCREMENT,
	kapazitaet INT UNSIGNED NOT NULL,
	anzahl_wiederholungen INT UNSIGNED NOT NULL,
	wiederholungs_zeitspanne INT UNSIGNED NOT NULL,
	PRIMARY KEY(p_fach_nr)
);
ALTER TABLE `Fragenkatalog`.`T_Faecher`
ADD COLUMN f_benutzer_nr INT UNSIGNED NOT NULL;

ALTER TABLE `Fragenkatalog`.`T_Faecher`
ADD CONSTRAINT cfk_benutzer_nr
FOREIGN KEY(`f_benutzer_nr`) REFERENCES `Fragenkatalog`.`T_Benutzer`(`p_benutzer_nr`)
ON UPDATE CASCADE ON DELETE CASCADE;


#--------------------------------------------------------------------------------------------------------------------------------------------
# Zugeordnet
#--------------------------------------------------------------------------------------------------------------------------------------------

DROP TABLE IF EXISTS `Fragenkatalog`.`T_Benutzer_Fragen_Faecher`;
CREATE TABLE `Fragenkatalog`.`T_Benutzer_Fragen_Faecher`
(
	p_f_benutzer_nr INT UNSIGNED NOT NULL,
	p_f_frage_nr INT UNSIGNED NOT NULL,
	p_f_fach_nr INT UNSIGNED NOT NULL,
	richtig INT UNSIGNED NOT NULL,
	falsch INT UNSIGNED NOT NULL,
	PRIMARY KEY(p_f_benutzer_nr, p_f_frage_nr, p_f_fach_nr)
);

ALTER TABLE `Fragenkatalog`.`T_Benutzer_Fragen_Faecher`
ADD CONSTRAINT cfk_zugeordnet_benutzer_nr
FOREIGN KEY(`p_f_benutzer_nr`) REFERENCES `Fragenkatalog`.`T_Benutzer`(`p_benutzer_nr`)
ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE `Fragenkatalog`.`T_Benutzer_Fragen_Faecher`
ADD CONSTRAINT cfk_zugeordnet_frage_nr
FOREIGN KEY(`p_f_frage_nr`) REFERENCES `Fragenkatalog`.`T_Fragen`(`p_frage_nr`)
ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE `Fragenkatalog`.`T_Benutzer_Fragen_Faecher`
ADD CONSTRAINT cfk_zugeordnet_fach_nr
FOREIGN KEY(`p_f_fach_nr`) REFERENCES `Fragenkatalog`.`T_Faecher`(`p_fach_nr`)
ON UPDATE CASCADE ON DELETE CASCADE;
