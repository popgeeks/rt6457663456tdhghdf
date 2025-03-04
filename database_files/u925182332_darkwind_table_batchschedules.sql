
-- --------------------------------------------------------

--
-- Table structure for table `batchschedules`
--

CREATE TABLE `batchschedules` (
  `BatchSchedulesID` int(10) UNSIGNED NOT NULL,
  `BatchCommandID` varchar(45) NOT NULL,
  `BatchScheduleRunTime` datetime NOT NULL,
  `BatchCompleted` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `batchschedules`
--

INSERT INTO `batchschedules` (`BatchSchedulesID`, `BatchCommandID`, `BatchScheduleRunTime`, `BatchCompleted`) VALUES
(1, '6', '2011-03-31 23:59:59', 1),
(2, '6', '2011-04-30 23:55:00', 1),
(3, '6', '2011-05-31 23:55:00', 1),
(4, '6', '2011-06-30 23:55:00', 1),
(5, '6', '2011-07-31 23:55:00', 1),
(6, '6', '2011-08-31 23:55:00', 1),
(7, '6', '2011-09-30 23:55:00', 1),
(8, '6', '2011-10-31 23:55:00', 1),
(9, '6', '2011-11-30 23:55:00', 1),
(10, '6', '2011-12-31 23:55:00', 1),
(11, '6', '2011-02-28 23:55:00', 1),
(12, '6', '2012-07-01 23:55:00', 1),
(13, '6', '2013-02-28 23:55:00', 1),
(14, '6', '2013-03-31 23:55:00', 1),
(15, '6', '2013-04-30 23:55:00', 1),
(16, '6', '2013-05-31 23:55:00', 1),
(17, '6', '2013-06-30 23:55:00', 1),
(18, '6', '2013-07-31 23:55:00', 1),
(19, '6', '2013-08-31 23:55:00', 1),
(20, '6', '2013-09-30 23:55:00', 1),
(21, '6', '2013-10-31 23:55:00', 1),
(22, '6', '2013-11-30 23:55:00', 1),
(23, '6', '2013-12-31 23:55:00', 1);
