
-- --------------------------------------------------------

--
-- Table structure for table `games_lu`
--

CREATE TABLE `games_lu` (
  `idGames` int(11) NOT NULL,
  `GameKeyword` varchar(45) DEFAULT NULL,
  `GameDescription` varchar(100) DEFAULT NULL,
  `GameTypeSeo` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `games_lu`
--

INSERT INTO `games_lu` (`idGames`, `GameKeyword`, `GameDescription`, `GameTypeSeo`) VALUES
(1, 'TT', 'Triple Triad', 'Triple-Triad'),
(2, 'OTT', 'Omni Triple Triad', 'Omni-Triple-Triad'),
(3, 'SB', 'Sphere Break', 'Sphere-Break'),
(4, 'TTM', 'Triple Triad Memory', 'Triple-Triad-Memory'),
(5, 'TTW', 'Triple Triad War', 'Triple-Triad-War'),
(6, 'CW', 'Triple Triad War', 'Triple-Triad-War'),
(7, 'CHIN', 'Chinchirorin', 'Chinchirorin'),
(8, 'TOTT', 'Team Omni Triple Triad', 'Team-Omni-Triple-Triad');
