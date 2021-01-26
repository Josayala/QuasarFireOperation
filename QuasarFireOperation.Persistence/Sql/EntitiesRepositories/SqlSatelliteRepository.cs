using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;
using QuasarFireOperation.Domain.CommandModel.Entities;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.QueryModel.Dtos;

namespace QuasarFireOperation.Persistence.Sql.EntitiesRepositories
{
    public class SqlSatelliteRepository : SqlRepositoryBase, ISatelliteRepository
    {
        private readonly List<Point> pointList = new List<Point>();

        private readonly List<Satellite> satellitesInformation = new List<Satellite>
        {
            new Satellite
            {
                Name = "kenobi",
                PositionX = -19.6685,
                PositionY = -69.1942
            },
            new Satellite
            {
                Name = "skywalker",
                PositionX = -20.2705,
                PositionY = -70.1311
            },
            new Satellite
            {
                Name = "sato",
                PositionX = -20.5656,
                PositionY = -70.1807
            }
        };

        public SqlSatelliteRepository(SqlUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public SatelliteMessageDto FindInformation(List<AddSatelliteObject> satelliteList)
        {
            if (satelliteList == null || satelliteList.Count <= 2)
                throw new ArgumentOutOfRangeException(
                    "There are not enough satellites to calculate the position of the spaceShip");

            var position = GetLocation(satelliteList);
            var message = GetMessage(satelliteList);
            var satelliteMessage = new SatelliteMessageDto(position, message);
            return satelliteMessage;
        }

        public Position GetLocation(List<AddSatelliteObject> satelliteList)
        {
            foreach (var satellite in satelliteList)
            {
                var satInfo = satellitesInformation.FirstOrDefault(x => x.Name == satellite.Name);
                if (satInfo != null)
                    pointList.Add(new Point(satInfo.PositionX, satInfo.PositionY, satellite.Distance));
            }

            var position =
                Trilateration.Compute(pointList.ElementAt(0), pointList.ElementAt(1), pointList.ElementAt(2));
            if (position != null)
                return new Position(position[0], position[1]);
            return null;
        }

        public string GetMessage(List<AddSatelliteObject> satelliteList)
        {
            var isCompletedMessage = false;
            var hasOverlappingWord = false;
            var messageIssued = new StringBuilder("");
            var lengthMessage = 0;

            if (satelliteList != null) lengthMessage = GetLengthMessage(satelliteList);


            var outPutMessage = new string[lengthMessage];

            var occupiedPositionList = new List<int>();


            foreach (var satellite in satelliteList)
            {
                if (HasAllMessages(satellite.Message, lengthMessage))
                {
                    for (var k = lengthMessage; k >= 0; k--)
                        outPutMessage[k] = satellite.Message.ElementAt(k);
                    isCompletedMessage = true;
                }

                var i = satellite.Message.Count - 1;
                var positionWord = lengthMessage - 1;
                while (i >= 0 && positionWord >= 0 && occupiedPositionList.Count != lengthMessage)
                {
                    var completedWord = satellite.Message.ElementAt(i);

                    if (!string.IsNullOrEmpty(completedWord) && !occupiedPositionList.Contains(positionWord))
                    {
                        occupiedPositionList.Add(positionWord);
                        outPutMessage[positionWord] = completedWord;
                    }

                    if (!string.IsNullOrEmpty(completedWord) && outPutMessage[positionWord] != completedWord)
                    {
                        hasOverlappingWord = true;
                        i = -1;
                    }
                    else
                    {
                        positionWord--;
                        i--;
                    }
                }

                if (occupiedPositionList.Count == lengthMessage) isCompletedMessage = true;

                if (isCompletedMessage || hasOverlappingWord) break;
            }

            if (isCompletedMessage)
                for (var i = 0; i < outPutMessage.Length; i++)
                {
                    messageIssued.Append(outPutMessage[i]);
                    messageIssued.Append(" ");
                }

            return messageIssued.ToString();
        }

        private bool HasAllMessages(List<string> messageList, int lengthMessage)
        {
            var hasMessage = true;

            for (var i = messageList.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(messageList.ElementAt(i)) && i <= lengthMessage) hasMessage = false;

                if (!hasMessage) break;
            }

            return hasMessage;
        }


        private int GetLengthMessage(List<AddSatelliteObject> satelliteList)
        {
            var minLength = 10000000;
            foreach (var satellite in satelliteList)
                if (minLength > satellite.Message.Count)
                    minLength = satellite.Message.Count;
            return minLength;
        }
    }
}