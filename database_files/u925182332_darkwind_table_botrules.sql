
-- --------------------------------------------------------

--
-- Table structure for table `botrules`
--

CREATE TABLE `botrules` (
  `id` int(10) UNSIGNED NOT NULL,
  `rules` varchar(200) NOT NULL DEFAULT '',
  `gametype` varchar(6) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `botrules`
--

INSERT INTO `botrules` (`id`, `rules`, `gametype`) VALUES
(1, 'Random/1', 'OTT'),
(2, 'Random/3', 'OTT'),
(3, 'Random/5', 'OTT'),
(4, 'Random/7', 'OTT'),
(5, 'Player/1', 'OTT'),
(6, 'Player/3', 'OTT'),
(7, 'Player/5', 'OTT'),
(8, 'Player/7', 'OTT'),
(9, 'Balamb - (Open) + Random', 'TT'),
(10, 'Dollet - (Random, Elemental)', 'TT'),
(11, 'Galbadia - (Same, Combo)', 'TT'),
(12, 'FH - (Elemental, Sudden Death)', 'TT'),
(13, 'Esthar - (Wall, Elemental, Combo)', 'TT'),
(14, 'Trabia - (Random, Wall, Combo)', 'TT'),
(15, 'Centra - (Open, Same, Combo, Plus)', 'TT'),
(16, 'Lunar - (Random, Open, Same, Wall, Elemental, Combo, Plus, Sudden Death)', 'TT'),
(17, 'AtomsXCross', 'TT'),
(18, 'ToC + Max Lvl 7', 'TT'),
(19, 'ToC + Max Lvl 8', 'TT'),
(20, 'ToC + Max Lvl 9', 'TT'),
(21, 'ToC + Cross', 'TT'),
(22, 'Standard Rules', 'TTM');
