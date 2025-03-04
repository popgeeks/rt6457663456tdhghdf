
-- --------------------------------------------------------

--
-- Table structure for table `elements_lu`
--

CREATE TABLE `elements_lu` (
  `ID` int(10) UNSIGNED NOT NULL,
  `Element` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `elements_lu`
--

INSERT INTO `elements_lu` (`ID`, `Element`) VALUES
(1, 'Earth'),
(2, 'Fire'),
(3, 'Holy'),
(4, 'Ice'),
(5, 'Thunder'),
(6, 'Mechanical'),
(7, 'Poison'),
(8, 'Shadow'),
(9, 'Water'),
(10, 'Wind');
