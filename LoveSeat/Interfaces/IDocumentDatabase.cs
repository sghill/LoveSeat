﻿using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace LoveSeat.Interfaces
{
    public interface IDocumentDatabase
    {
        /// <summary>
        /// Creates a document using the json provided. 
        /// No validation or smarts attempted here by design for simplicities sake
        /// </summary>
        /// <param name="id">Id of Document</param>
        /// <param name="jsonForDocument"></param>
        /// <returns>The status from CouchDb as a JObject</returns>
        JObject CreateDocument(string id, string jsonForDocument);

        JObject CreateDocument(Document doc);

        /// <summary>
        /// Creates a document when you intend for Couch to generate the id for you.
        /// </summary>
        /// <param name="jsonForDocument">Json for creating the document</param>
        /// <returns>Returns the status from Couchdb as a JObject</returns>
        JObject CreateDocument(string jsonForDocument);

        JObject DeleteDocument(string id, string rev);

        /// <summary>
        /// Returns null if document is not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Document GetDocument(string id);

        T GetDocument<T>(string id);
        T GetDocument<T>(string id, IObjectSerializer<T> objectSerializer);

        /// <summary>
        /// Adds an attachment to a document.  If revision is not specified then the most recent will be fetched and used.  Warning: if you need document update conflicts to occur please use the method that specifies the revision
        /// </summary>
        /// <param name="id">id of the couch Document</param>
        /// <param name="attachment">byte[] of of the attachment.  Use File.ReadAllBytes()</param>
        /// <param name="contentType">Content Type must be specifed</param>	
        JObject AddAttachment(string id, byte[] attachment, string filename, string contentType);

        /// <summary>
        /// Adds an attachment to the documnet.  Rev must be specified on this signature.  If you want to attach no matter what then use the method without the rev param
        /// </summary>
        /// <param name="id">id of the couch Document</param>
        /// <param name="rev">revision _rev of the Couch Document</param>
        /// <param name="attachment">byte[] of of the attachment.  Use File.ReadAllBytes()</param>
        /// <param name="filename">filename of the attachment</param>
        /// <param name="contentType">Content Type must be specifed</param>			
        /// <returns></returns>
        JObject AddAttachment(string id, string rev, byte[] attachment, string filename, string contentType);

        Stream GetAttachmentStream(Document doc, string attachmentName);
        Stream GetAttachmentStream(string docId, string rev, string attachmentName);
        Stream GetAttachmentStream(string docId, string attachmentName);
        JObject DeleteAttachment(string id, string rev, string attachmentName);
        JObject DeleteAttachment(string id, string attachmentName);
        JObject SaveDocument(Document document);

        /// <summary>
        /// Gets the results of a view with no view parameters.  Use the overload to pass parameters
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="designDoc">The design doc on which the view resides</param>
        /// <returns></returns>
        ViewResult<T> View<T>(string viewName, string designDoc);

        /// <summary>
        /// Gets the results of the view using any and all parameters
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Options such as startkey etc.</param>
        /// <param name="designDoc">The design doc on which the view resides</param>
        /// <returns></returns>
        ViewResult<T> View<T>(string viewName, ViewOptions options, string designDoc);

        /// <summary>
        /// Don't use this overload unless you intend to override the default ObjectSerialization behavior.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewName"></param>
        /// <param name="options"></param>
        /// <param name="designDoc"></param>
        /// <param name="objectSerializer">Only needed unless you'd like to override the default behavior of the serializer</param>
        /// <returns></returns>
        ViewResult<T> View<T>(string viewName, ViewOptions options, string designDoc, IObjectSerializer<T> objectSerializer);

        /// <summary>
        /// Gets all the documents in the database using the _all_docs uri
        /// </summary>
        /// <returns></returns>
        ViewResult GetAllDocuments();

        ViewResult GetAllDocuments(ViewOptions options);

        /// <summary>
        /// Gets the results of the view using the defaultDesignDoc and no view parameters.  Use the overloads to specify options.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewName"></param>
        /// <returns></returns>
        ViewResult<T> View<T>(string viewName);

        /// <summary>
        /// Allows you to specify options and uses the defaultDesignDoc Specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewName"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ViewResult<T>  View<T>(string viewName, ViewOptions options);

        /// <summary>
        /// Allows you to override the objectSerializer and use the Default Design Doc settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewName"></param>
        /// <param name="options"></param>
        /// <param name="objectSerializer"></param>
        /// <returns></returns>
        ViewResult<T> View<T>(string viewName, ViewOptions options, IObjectSerializer<T> objectSerializer);

        T GetDocument<T>(Guid id , IObjectSerializer<T> objectSerializer);
        T GetDocument<T>(Guid id);
        string Show (string showName, string docId);
        IListResult List(string listName, string viewName, ViewOptions options,  string designDoc);
        IListResult List(string listName, string viewName, ViewOptions options);

        ViewResult View(string viewName, ViewOptions options);
        ViewResult View(string viewName);
    }
}