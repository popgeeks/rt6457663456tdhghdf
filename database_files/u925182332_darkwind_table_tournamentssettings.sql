
-- --------------------------------------------------------

--
-- Table structure for table `tournamentssettings`
--

CREATE TABLE `tournamentssettings` (
  `TournamentsSettingsID` int(11) UNSIGNED NOT NULL,
  `TournamentsSettingsRake` decimal(11,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tournamentssettings`
--

INSERT INTO `tournamentssettings` (`TournamentsSettingsID`, `TournamentsSettingsRake`) VALUES
(1, 0.10);
