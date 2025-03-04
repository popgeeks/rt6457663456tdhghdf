
-- --------------------------------------------------------

--
-- Table structure for table `tournamentspayoutstructure`
--

CREATE TABLE `tournamentspayoutstructure` (
  `tournamentspayoutstructureID` int(10) UNSIGNED NOT NULL,
  `tournamentspayoutstructureplace` int(10) UNSIGNED NOT NULL,
  `tournamentspayoutstructurepercentage` decimal(10,2) NOT NULL,
  `tournamentspayoutstructuremaxplayers` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tournamentspayoutstructure`
--

INSERT INTO `tournamentspayoutstructure` (`tournamentspayoutstructureID`, `tournamentspayoutstructureplace`, `tournamentspayoutstructurepercentage`, `tournamentspayoutstructuremaxplayers`) VALUES
(1, 1, 0.60, 6),
(2, 2, 0.40, 6),
(4, 1, 0.50, 10),
(5, 2, 0.30, 10),
(6, 3, 0.20, 10),
(7, 1, 1.00, 2),
(8, 1, 0.65, 4),
(9, 1, 0.35, 4),
(10, 1, 0.50, 9),
(11, 1, 0.30, 9),
(12, 1, 0.20, 9);
