
-- --------------------------------------------------------

--
-- Table structure for table `tournamentstypes`
--

CREATE TABLE `tournamentstypes` (
  `TournamentsTypesID` int(11) UNSIGNED NOT NULL,
  `TournamentsTypesDescription` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tournamentstypes`
--

INSERT INTO `tournamentstypes` (`TournamentsTypesID`, `TournamentsTypesDescription`) VALUES
(1, 'Single Elimination'),
(2, 'Double Elimination'),
(3, 'RPG Elimination'),
(4, 'Defender'),
(5, 'Group-Defender'),
(6, '3 and Out'),
(7, 'Progressive RPG'),
(8, 'Tournament of Champions');
