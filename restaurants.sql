-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Oct 27, 2017 at 01:38 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `restaurants`
--
CREATE DATABASE IF NOT EXISTS `restaurants` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `restaurants`;

-- --------------------------------------------------------

--
-- Table structure for table `cuisine`
--

CREATE TABLE `cuisine` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Dumping data for table `cuisine`
--

INSERT INTO `cuisine` (`id`, `name`) VALUES
(1, 'Sushi'),
(2, 'Mexican'),
(3, 'Filipino'),
(5, 'Italian'),
(6, 'Chinese'),
(7, 'dsfsdf');

-- --------------------------------------------------------

--
-- Table structure for table `cuisine1`
--

CREATE TABLE `cuisine1` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Dumping data for table `cuisine1`
--

INSERT INTO `cuisine1` (`id`, `name`) VALUES
(0, 'Chinese'),
(0, 'Chinese'),
(0, 'Chinese'),
(0, 'Ugh'),
(0, 'dslkfj'),
(0, 'xzcvx'),
(0, 'dsfdsf'),
(0, 'Mexican');

-- --------------------------------------------------------

--
-- Table structure for table `restaurants`
--

CREATE TABLE `restaurants` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  `cuisine_id` int(11) NOT NULL,
  `price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Dumping data for table `restaurants`
--

INSERT INTO `restaurants` (`id`, `name`, `type`, `cuisine_id`, `price`) VALUES
(2, 'dsfsd', 'sdfdsf', 0, 3),
(3, 'Herfy\'s Heffers', 'Burgers', 0, 1),
(4, 'Chow King', 'Food and Shit', 3, 1),
(5, 'Trattoria Aldini', 'Black Magic Fuckery', 4, 3),
(7, 'China Gate', 'Comfort food', 6, 2),
(8, 'Jalisco\'s', 'Tacos', 2, 1),
(9, 'fsdfsa', 'dsfsdf', 3, 1);

-- --------------------------------------------------------

--
-- Table structure for table `reviews`
--

CREATE TABLE `reviews` (
  `id` int(11) NOT NULL,
  `description` varchar(255) NOT NULL,
  `reviewer_name` varchar(255) NOT NULL,
  `restaurant_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `reviews`
--

INSERT INTO `reviews` (`id`, `description`, `reviewer_name`, `restaurant_id`) VALUES
(1, 'xcssgdsvd', 'dfdsf', 3),
(2, 'sdfsdf', 'dfdsf', 3),
(3, 'sdfsdf', 'dfdsf', 3),
(5, 'asdfds', 'sdfasdf', 5),
(6, 'sdfasdf', 'dsfsadf', 2),
(7, 'dsfasdf', 'sdfsd', 2),
(8, 'czxc', 'xzczxc', 8);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cuisine`
--
ALTER TABLE `cuisine`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `restaurants`
--
ALTER TABLE `restaurants`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `reviews`
--
ALTER TABLE `reviews`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cuisine`
--
ALTER TABLE `cuisine`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `restaurants`
--
ALTER TABLE `restaurants`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `reviews`
--
ALTER TABLE `reviews`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
