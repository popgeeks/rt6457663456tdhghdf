
-- --------------------------------------------------------

--
-- Table structure for table `guild_applications`
--

CREATE TABLE `guild_applications` (
  `id` int(10) UNSIGNED NOT NULL,
  `guild` varchar(50) NOT NULL,
  `player` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `guild_applications`
--

INSERT INTO `guild_applications` (`id`, `guild`, `player`) VALUES
(11, 'The Turks', 'Doomtrain99'),
(92, 'Dark Creation', 'pokojijung'),
(167, 'Operation Delta', 'Amers'),
(180, 'SeeD', 'neppius'),
(233, 'SeeD', 'Semaphore'),
(241, 'SeeD', '7acsephiroth'),
(300, 'Triple Triad Legends', 'PDub'),
(441, 'The One', 'archfeind'),
(621, 'Sama-San', 'Tensu'),
(772, 'Guild of Legends', 'toukka'),
(864, 'SeeD', 'xdarksquallx'),
(886, 'SeeD', 'squall'),
(933, 'Guild of Legends', 'quelhunter'),
(963, 'SeeD', 'TrueLugia121'),
(1034, 'SeeD', 'barrybright'),
(1075, 'The One', 'Laner'),
(1148, 'LegendsFaith', 'henryg'),
(1276, 'SeeD', 'BFulks'),
(1284, 'Forest Owls', 'sorceress'),
(1288, 'Card Club', 'belmont61'),
(1300, 'Tonberry Kings', 'vincemalta10'),
(1417, 'SeeD', 'valkyrie'),
(1421, 'Tonberry Kings', 'qball'),
(1443, 'geo-stigma', 'Blak'),
(1524, 'SeeD', 'Rochondil'),
(1573, 'Warrior Nation', 'vespoid89'),
(1613, 'La Perla Negra', 'PiPpO'),
(1712, 'Deep  Dungeon', 'bum'),
(1799, 'Card Club', 'Desperado'),
(1807, 'SeeD', 'DDonSeeD'),
(1821, 'La Perla Negra', 'SilverShadow'),
(1962, 'Ad Arcana', 'onyzuka'),
(1992, 'SeeD', 'MitchellDyer'),
(2041, 'Straw Hat Pirates', 'delangoc'),
(2288, 'SeeD', 'kicknass'),
(2332, '501st', 'zeroin'),
(2485, 'Foxhound', 'AlucarDNyx'),
(2592, 'Kingdom Cards', 'CherubUltima'),
(2672, 'Forest Owls', 'Rinoa1'),
(2675, 'FF_Ksenomorphs', 'elrine'),
(2783, 'Servants of Darkness', 'Kadajiroth'),
(2789, 'FF_Ksenomorphs', 'NYSHALYNN'),
(2816, 'Deep  Dungeon', 'iriqi'),
(2839, 'Arcane Empire', 'chaosmage3'),
(2843, 'Kingdom Cards', 'Bors'),
(2848, 'FF_Ksenomorphs', 'praygon'),
(2884, 'SeeD', 'Jungleberry'),
(2905, 'CC Group', 'HMSquall'),
(2925, 'Knights Of Gaia', 'KylarStern'),
(2928, 'OtherWorld', 'roso'),
(2941, 'SeeD', 'Merric'),
(2991, 'Servants of Darkness', 'looney36'),
(3014, 'Knights Of Gaia', 'MalboroFreak'),
(3017, 'Trodain Empire', 'elvoret659'),
(3075, 'SeeD', 'Zell8778'),
(3104, 'Kingdom Cards', 'simone0'),
(3106, 'NERV Reborn', 'gulasz'),
(3107, 'The Elite', 'AwesomeNo1'),
(3122, 'Knights Of Gaia', 'giulio'),
(3123, 'Chaos Order', 'edzeal'),
(3124, '501st ', 'sahil'),
(3125, '501st ', 'sahil'),
(3126, 'Knights of Dawn', 'Ifrit123');
