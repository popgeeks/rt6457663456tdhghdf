
-- --------------------------------------------------------

--
-- Table structure for table `tt_gamecards`
--

CREATE TABLE `tt_gamecards` (
  `GameID` int(10) NOT NULL,
  `Player1CardsReady` int(2) DEFAULT 0,
  `Player2CardsReady` int(2) DEFAULT 0,
  `cards-plr1_0` varchar(75) DEFAULT '',
  `cards-plr1_1` varchar(45) DEFAULT '',
  `cards-plr1_2` varchar(45) DEFAULT '',
  `cards-plr1_3` varchar(45) DEFAULT '',
  `cards-plr1_4` varchar(45) DEFAULT '',
  `cards-plr2_0` varchar(45) DEFAULT '',
  `cards-plr2_1` varchar(45) DEFAULT '',
  `cards-plr2_2` varchar(45) DEFAULT '',
  `cards-plr2_3` varchar(45) DEFAULT '',
  `cards-plr2_4` varchar(45) DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_general_ci;

--
-- Dumping data for table `tt_gamecards`
--

INSERT INTO `tt_gamecards` (`GameID`, `Player1CardsReady`, `Player2CardsReady`, `cards-plr1_0`, `cards-plr1_1`, `cards-plr1_2`, `cards-plr1_3`, `cards-plr1_4`, `cards-plr2_0`, `cards-plr2_1`, `cards-plr2_2`, `cards-plr2_3`, `cards-plr2_4`) VALUES
(2811427, 1, 1, '0', '0', 'Servant of Darkness', '0', '0', '0', '0', '0', '0', '0'),
(2811431, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', 'FF5 Double Lizard', '0'),
(2811432, 1, 1, 'Christmas Serge', 'Eight Gentle Judges', 'Great Demon', 'Maria, Draco', 'FF5 Ultra Gigas', 'Nigi Mitama', 'Morrigan Aensland', 'Firion', 'Valaha', 'FF5 La Mage'),
(2811433, 1, 1, '0', '0', '0', '0', '0', 'Always Within', '0', '0', '0', '0'),
(2811434, 1, 1, '0', '0', '0', '0', '0', '0', '0', 'Mithra', 'Unholy Bishop', '0'),
(2811435, 1, 1, 'Hydra', 'Mt Hobbs Gargoyle', 'Banjo-Kazooie', 'Vampire Thorn', 'Frieza', 'Crawler', 'E-123 Omega', 'Cyber Hacker Clock', 'The Illusion', 'Horn'),
(2811438, 1, 1, '0', 'Shellmon', 'Riza Hawkeye', 'Spearook', 'Miltank', 'Armorer Samus', 'Tir Mcdohl', 'Garland', 'Aeon Ixion', 'Porygon'),
(2811439, 1, 1, 'FOmarl', 'Rebel Valeria', 'Hook Bat', 'Beowulf Kadmas', 'Touch Me', 'Sledgehammer', 'Saito Hajime', 'Zeromus', 'Dragoyle', 'Karen Wong'),
(2811440, 1, 1, '0', '0', '0', '0', '0', '0', 'Vegnagun', '0', '0', '0'),
(2811441, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', '0', 'Snorlax'),
(2811442, 1, 1, 'Dual Heads', 'A Monk', 'PokeNav', 'TestCard1', 'Nightbane', 'FFTA Shiva', 'Naruto', 'Bull Man', 'Gin', 'Human Fighter'),
(2811443, 1, 1, '0', '0', '0', 'Weepy Eye', 'Hammerhead', '0', '0', '0', 'FF5 Odin', 'Bayman'),
(2811444, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', 'Duke DeFaux', '0'),
(2811445, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', '0', 'ID'),
(2811446, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', '0', 'Genesis Jinn'),
(2811447, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', '0', 'Trickster Belle'),
(2811448, 1, 1, '0', '0', '0', '0', '0', '0', '0', 'Slaking', '0', '0'),
(2811452, 0, 1, '', '', '', '', '', 'Pantheon Hopper', 'Air Soldier', 'PuPu', 'Sabretooth', 'Trauma'),
(2811453, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', 'Mokumon', '0'),
(2811455, 1, 1, '0', '0', '0', '0', '0', '0', 'Scout', '0', '0', '0'),
(2811456, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', '0', 'Summon Ifrit'),
(2811466, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', 'Furret', 'Seta Noriyasu'),
(2811480, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', 'FF5 Forza', '0'),
(2811483, 1, 1, 'Fafnir', 'Tina Armstrong', 'Dragon Emblem', 'Guard Armour', 'Kael-thas Sunstrider', 'May', 'Sneasel', 'Nevado', 'Spider-Man', 'Aeon Yojimbo'),
(2811485, 1, 1, 'Gandar', 'Garland', 'Lindwyrm', 'Doom', 'Aeon Bahamut', 'Kaiwan', 'Rinoa', 'Bone Dragon', 'Cave Naga', 'Talim'),
(2811489, 1, 1, '0', 'Beowulf Kadmas', '0', '0', '0', '0', '0', 'Shikome', '0', '0'),
(2811490, 1, 1, '0', '0', '0', '0', 'Mystic Powers', '0', '0', 'Shark Tooth', '0', '0'),
(2811491, 1, 1, '0', '0', '0', 'Stantlers Guidance', '0', '0', '0', '0', '0', 'Mew'),
(2811492, 1, 1, '0', 'Magby', 'Kratos', 'Esper Maduin', 'Behemoth, the Coliseum Monster', '0', 'Deku Scrub', 'Tropius', 'Tentolancer', 'Super Tiamat'),
(2811493, 1, 1, '0', '0', '0', '0', 'Great Fox', '0', '0', '0', '0', 'Unsent Love'),
(2811494, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', 'Crusnik Abel', '0'),
(2811495, 1, 1, '0', '0', '0', '0', '0', '0', '0', '0', '0', 'Imperial Invasion');
