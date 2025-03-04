
-- --------------------------------------------------------

--
-- Table structure for table `cardwars_elementmods`
--

CREATE TABLE `cardwars_elementmods` (
  `ID` int(10) UNSIGNED NOT NULL,
  `AtkElementID` int(10) UNSIGNED NOT NULL,
  `DefElementID` int(10) UNSIGNED NOT NULL,
  `DamageMod` decimal(10,2) NOT NULL DEFAULT 1.00
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `cardwars_elementmods`
--

INSERT INTO `cardwars_elementmods` (`ID`, `AtkElementID`, `DefElementID`, `DamageMod`) VALUES
(1, 1, 1, 0.50),
(2, 2, 2, 0.50),
(3, 3, 3, 0.50),
(4, 4, 4, 0.50),
(5, 5, 5, 0.50),
(6, 6, 6, 0.50),
(7, 7, 7, 0.50),
(8, 8, 8, 0.50),
(9, 10, 10, 0.50),
(10, 1, 6, 2.00),
(11, 1, 10, 0.80),
(12, 2, 4, 2.00),
(13, 2, 9, 0.00),
(14, 2, 7, 0.50),
(15, 2, 7, 0.75),
(16, 3, 8, 2.00),
(17, 4, 2, 2.00),
(18, 4, 9, 0.50),
(19, 5, 4, 1.50),
(20, 5, 9, 2.00),
(21, 5, 2, 0.50),
(22, 6, 1, 2.00),
(23, 6, 7, 0.75),
(24, 7, 3, 1.50),
(25, 8, 3, 2.00),
(26, 9, 5, 2.00),
(27, 9, 2, 0.00),
(28, 9, 4, 0.50),
(29, 10, 1, 0.50),
(30, 9, 9, 0.50);
