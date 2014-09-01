﻿namespace Raven.Documentation.Samples.ClientApi.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Abstractions.Util;
	using Client.Connection;
	using Client.Connection.Async;
	using Client.Converters;
	using Client.Document;
	using CodeSamples;
	using CodeSamples.Orders;
	using GuidConverter = Client.Converters.GuidConverter;
	using Int32Converter = Client.Converters.Int32Converter;
	using Int64Converter = Client.Converters.Int64Converter;

	public class Conventions
	{
		#region custom_converter
		public class UInt32Converter : ITypeConverter
		{
			public bool CanConvertFrom(Type sourceType)
			{
				throw new CodeOmitted();
			}

			public string ConvertFrom(string tag, object value, bool allowNull)
			{
				throw new CodeOmitted();
			}

			public object ConvertTo(string value)
			{
				throw new CodeOmitted();
			}
		}
		#endregion

		public void GeneralConventions()
		{
			var store = new DocumentStore();

			#region conventions_1
			DocumentConvention Conventions = store.Conventions;
			#endregion

			#region key_generator_hilo
			var hiLoGenerator = new MultiDatabaseHiLoGenerator(32);
			Conventions.DocumentKeyGenerator = (dbName, databaseCommands, entity) => 
								hiLoGenerator.GenerateDocumentKey(dbName, databaseCommands, Conventions, entity);

			var asyncHiLoGenerator = new AsyncMultiDatabaseHiLoKeyGenerator(32);
			Conventions.AsyncDocumentKeyGenerator = (dbName, commands, entity) => 
								asyncHiLoGenerator.GenerateDocumentKeyAsync(dbName, commands, Conventions, entity);
			#endregion

			#region key_generator_identityKeys
			Conventions.DocumentKeyGenerator = (dbname, commands, entity) =>
								store.Conventions.GetTypeTagName(entity.GetType()) + "/";
			#endregion

			#region find_type_name
			Conventions.FindClrTypeName = type => // use reflection to determine the type;
			#endregion
				string.Empty;

			#region find_clr_type
			Conventions.FindClrType = (id, doc, metadata) =>
								metadata.Value<string>(Abstractions.Data.Constants.RavenClrType);
			#endregion

			#region find_type_tagname
			Conventions.FindTypeTagName = type => // function that provides the collection name based on the entity type
			#endregion
				string.Empty;

			#region find_dynamic_tag_name
			Conventions.FindDynamicTagName = dynamicObject => // function to determine the collection name for the given dynamic object
			#endregion	
				string.Empty;

			#region find_identity_property
			Conventions.FindIdentityProperty = memberInfo => memberInfo.Name == "Id";
			#endregion

			#region find_iden_propn_name_from_entity_name
			Conventions.FindIdentityPropertyNameFromEntityName = entityName => "Id";
			#endregion

			#region identity_part_separator
			Conventions.IdentityPartsSeparator = "/";
			#endregion

			#region identity_type_convertors
			Conventions.IdentityTypeConvertors = new List<ITypeConverter>
			{
				new GuidConverter(),
				new Int32Converter(),
				new Int64Converter()
			};
			#endregion

			#region identity_type_convertors_2
			Conventions.IdentityTypeConvertors.Add(new UInt32Converter());
			#endregion

			#region find_id_value_part_for_value_type_conversion
			Conventions.FindIdValuePartForValueTypeConversion = (entity, id) => 
				id.Split(new[] { Conventions.IdentityPartsSeparator }, StringSplitOptions.RemoveEmptyEntries).Last();
			#endregion

			#region find_full_doc_key_from_non_string_identifier
			Conventions.FindFullDocumentKeyFromNonStringIdentifier = (id, type, allowNull) => // by default returns [tagName]/[identityValue];
			#endregion
				string.Empty;

		}

		public interface IFoo
		{
			#region register_id_convention_method
			DocumentConvention RegisterIdConvention<TEntity>(Func<string, IDatabaseCommands, TEntity, string> func);
			#endregion

			#region register_id_convention_method_async
			DocumentConvention RegisterAsyncIdConvention<TEntity>(Func<string, IAsyncDatabaseCommands, TEntity, 
														Task<string>> func);
			#endregion
		}

		public void TypeSpecificConventions()
		{
			var store = new DocumentStore();

			#region eployees_custom_convention
			store.Conventions.RegisterIdConvention<Employee>(
				(dbname, commands, employee) => string.Format("employees/{0}/{1}", employee.LastName, employee.FirstName));
			#endregion

			#region eployees_custom_async_convention
			store.Conventions.RegisterAsyncIdConvention<Employee>(
				(dbname, commands, employee) => new CompletedTask<string>(
					string.Format("employees/{0}/{1}", employee.LastName, employee.FirstName)));
			#endregion

			#region eployees_custom_convention_example
			using (var session = store.OpenSession())
			{
				session.Store(new Employee
				{
					FirstName = "James",
					LastName = "Bond"
				});

				session.SaveChanges();
			}
			#endregion

			#region eployees_custom_convention_inheritance
			using (var session = store.OpenSession())
			{
				session.Store(new Employee // employees/Smith/Adam
				{
					FirstName = "Adam",
					LastName = "Smith"
				});

				session.Store(new EmployeeManager // employees/Jones/David
				{
					FirstName = "David",
					LastName = "Jones"
				});

				session.SaveChanges();
			}
			#endregion


			#region custom_convention_inheritance_2
			store.Conventions.RegisterIdConvention<Employee>(
				(dbname, commands, employee) => string.Format("employees/{0}/{1}", employee.LastName, employee.FirstName));

			store.Conventions.RegisterIdConvention<EmployeeManager>(
				(dbname, commands, employee) => string.Format("managers/{0}/{1}", employee.LastName, employee.FirstName));

			using (var session = store.OpenSession())
			{
				session.Store(new Employee // employees/Smith/Adam
				{
					FirstName = "Adam",
					LastName = "Smith"
				});

				session.Store(new EmployeeManager // managers/Jones/David
				{
					FirstName = "David",
					LastName = "Jones"
				});

				session.SaveChanges();
			}
			#endregion
		}

		public class EmployeeManager : Employee
		{
			 
		}

	}
}