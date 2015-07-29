﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PongServer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Pong")]
	public partial class PongDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPlayer(Player instance);
    partial void UpdatePlayer(Player instance);
    partial void DeletePlayer(Player instance);
    partial void InsertHighScore(HighScore instance);
    partial void UpdateHighScore(HighScore instance);
    partial void DeleteHighScore(HighScore instance);
    partial void InsertGame(Game instance);
    partial void UpdateGame(Game instance);
    partial void DeleteGame(Game instance);
    #endregion
		
		public PongDataContext() : 
				base(global::PongServer.Properties.Settings.Default.PongConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PongDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PongDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PongDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PongDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Player> Players
		{
			get
			{
				return this.GetTable<Player>();
			}
		}
		
		public System.Data.Linq.Table<HighScore> HighScores
		{
			get
			{
				return this.GetTable<HighScore>();
			}
		}
		
		public System.Data.Linq.Table<Game> Games
		{
			get
			{
				return this.GetTable<Game>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Player")]
	public partial class Player : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Username;
		
		private System.DateTime _LastLogin;
		
		private EntitySet<HighScore> _HighScores;
		
		private EntitySet<Game> _Games;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnLastLoginChanging(System.DateTime value);
    partial void OnLastLoginChanged();
    #endregion
		
		public Player()
		{
			this._HighScores = new EntitySet<HighScore>(new Action<HighScore>(this.attach_HighScores), new Action<HighScore>(this.detach_HighScores));
			this._Games = new EntitySet<Game>(new Action<Game>(this.attach_Games), new Action<Game>(this.detach_Games));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="NVarChar(40) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastLogin", DbType="DateTime NOT NULL")]
		public System.DateTime LastLogin
		{
			get
			{
				return this._LastLogin;
			}
			set
			{
				if ((this._LastLogin != value))
				{
					this.OnLastLoginChanging(value);
					this.SendPropertyChanging();
					this._LastLogin = value;
					this.SendPropertyChanged("LastLogin");
					this.OnLastLoginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_HighScore", Storage="_HighScores", ThisKey="Id", OtherKey="PlayerId")]
		public EntitySet<HighScore> HighScores
		{
			get
			{
				return this._HighScores;
			}
			set
			{
				this._HighScores.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game", Storage="_Games", ThisKey="Id", OtherKey="PlayerId")]
		public EntitySet<Game> Games
		{
			get
			{
				return this._Games;
			}
			set
			{
				this._Games.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_HighScores(HighScore entity)
		{
			this.SendPropertyChanging();
			entity.Player = this;
		}
		
		private void detach_HighScores(HighScore entity)
		{
			this.SendPropertyChanging();
			entity.Player = null;
		}
		
		private void attach_Games(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player = this;
		}
		
		private void detach_Games(Game entity)
		{
			this.SendPropertyChanging();
			entity.Player = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.HighScore")]
	public partial class HighScore : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _PlayerId;
		
		private int _Score;
		
		private int _PositionInRanking;
		
		private EntityRef<Player> _Player;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPlayerIdChanging(int value);
    partial void OnPlayerIdChanged();
    partial void OnScoreChanging(int value);
    partial void OnScoreChanged();
    partial void OnPositionInRankingChanging(int value);
    partial void OnPositionInRankingChanged();
    #endregion
		
		public HighScore()
		{
			this._Player = default(EntityRef<Player>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlayerId", DbType="Int NOT NULL")]
		public int PlayerId
		{
			get
			{
				return this._PlayerId;
			}
			set
			{
				if ((this._PlayerId != value))
				{
					if (this._Player.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPlayerIdChanging(value);
					this.SendPropertyChanging();
					this._PlayerId = value;
					this.SendPropertyChanged("PlayerId");
					this.OnPlayerIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Score", DbType="Int NOT NULL")]
		public int Score
		{
			get
			{
				return this._Score;
			}
			set
			{
				if ((this._Score != value))
				{
					this.OnScoreChanging(value);
					this.SendPropertyChanging();
					this._Score = value;
					this.SendPropertyChanged("Score");
					this.OnScoreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionInRanking", DbType="Int NOT NULL")]
		public int PositionInRanking
		{
			get
			{
				return this._PositionInRanking;
			}
			set
			{
				if ((this._PositionInRanking != value))
				{
					this.OnPositionInRankingChanging(value);
					this.SendPropertyChanging();
					this._PositionInRanking = value;
					this.SendPropertyChanged("PositionInRanking");
					this.OnPositionInRankingChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_HighScore", Storage="_Player", ThisKey="PlayerId", OtherKey="Id", IsForeignKey=true)]
		public Player Player
		{
			get
			{
				return this._Player.Entity;
			}
			set
			{
				Player previousValue = this._Player.Entity;
				if (((previousValue != value) 
							|| (this._Player.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player.Entity = null;
						previousValue.HighScores.Remove(this);
					}
					this._Player.Entity = value;
					if ((value != null))
					{
						value.HighScores.Add(this);
						this._PlayerId = value.Id;
					}
					else
					{
						this._PlayerId = default(int);
					}
					this.SendPropertyChanged("Player");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Game")]
	public partial class Game : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _PlayerId;
		
		private int _Score;
		
		private System.DateTime _Played;
		
		private EntityRef<Player> _Player;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPlayerIdChanging(int value);
    partial void OnPlayerIdChanged();
    partial void OnScoreChanging(int value);
    partial void OnScoreChanged();
    partial void OnPlayedChanging(System.DateTime value);
    partial void OnPlayedChanged();
    #endregion
		
		public Game()
		{
			this._Player = default(EntityRef<Player>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlayerId", DbType="Int NOT NULL")]
		public int PlayerId
		{
			get
			{
				return this._PlayerId;
			}
			set
			{
				if ((this._PlayerId != value))
				{
					if (this._Player.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPlayerIdChanging(value);
					this.SendPropertyChanging();
					this._PlayerId = value;
					this.SendPropertyChanged("PlayerId");
					this.OnPlayerIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Score", DbType="Int NOT NULL")]
		public int Score
		{
			get
			{
				return this._Score;
			}
			set
			{
				if ((this._Score != value))
				{
					this.OnScoreChanging(value);
					this.SendPropertyChanging();
					this._Score = value;
					this.SendPropertyChanged("Score");
					this.OnScoreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Played", DbType="DateTime NOT NULL")]
		public System.DateTime Played
		{
			get
			{
				return this._Played;
			}
			set
			{
				if ((this._Played != value))
				{
					this.OnPlayedChanging(value);
					this.SendPropertyChanging();
					this._Played = value;
					this.SendPropertyChanged("Played");
					this.OnPlayedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Player_Game", Storage="_Player", ThisKey="PlayerId", OtherKey="Id", IsForeignKey=true)]
		public Player Player
		{
			get
			{
				return this._Player.Entity;
			}
			set
			{
				Player previousValue = this._Player.Entity;
				if (((previousValue != value) 
							|| (this._Player.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Player.Entity = null;
						previousValue.Games.Remove(this);
					}
					this._Player.Entity = value;
					if ((value != null))
					{
						value.Games.Add(this);
						this._PlayerId = value.Id;
					}
					else
					{
						this._PlayerId = default(int);
					}
					this.SendPropertyChanged("Player");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
