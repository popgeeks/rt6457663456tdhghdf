
-- --------------------------------------------------------

--
-- Table structure for table `reserved`
--

CREATE TABLE `reserved` (
  `reserved_key` int(11) NOT NULL,
  `player` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `reserved`
--

INSERT INTO `reserved` (`reserved_key`, `player`) VALUES
(1, 'Atomsplitter'),
(2, 'AtomicNews'),
(3, 'AnimeNews'),
(4, 'Server');
