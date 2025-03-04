
-- --------------------------------------------------------

--
-- Table structure for table `accounting_lu`
--

CREATE TABLE `accounting_lu` (
  `ID` int(10) UNSIGNED NOT NULL,
  `Description` varchar(100) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `accounting_lu`
--

INSERT INTO `accounting_lu` (`ID`, `Description`) VALUES
(1, 'Gold Membership'),
(2, 'Platinum Membership'),
(3, 'Diamond Membership'),
(4, 'Power User Membership'),
(5, 'Interest/Dividends'),
(6, 'Advertisement'),
(7, 'Prizes'),
(8, 'Equipment'),
(9, 'Domains'),
(10, 'Investment'),
(11, 'Event Card Purchase'),
(12, 'Contract Work'),
(13, 'Utilities'),
(14, 'Research');
