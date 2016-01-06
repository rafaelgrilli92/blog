using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_4_lazy.Models
{
    public class Post
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Titulo { get; set; }

        [Required]
        public virtual string Descricao { get; set; }

        public virtual Categoria Categoria { get; set; }

        [Required]
        public virtual bool Publicado { get; set; }

        [AllowHtml]
        public virtual string Conteudo { get; set; }

        public virtual string Imagem { get; set; }
        
        public virtual DateTime? DataPublicacao { get; set; }

        public virtual int Visualizacoes { get; set; }
        public virtual int Likes { get; set; }

    }

    class PostMapping : ClassMap<Post>
    {
        public PostMapping()
        {
            Table("Posts");
            Id(post => post.Id).GeneratedBy.Identity();
            Map(post => post.Titulo);
            Map(post => post.Descricao);
            References(p => p.Categoria, "CategoriaId");
            Map(post => post.Publicado);
            Map(post => post.Conteudo).CustomSqlType("LONGTEXT");
            //Map(post => post.Imagem);
            Map(post => post.DataPublicacao).CustomSqlType("TIMESTAMP").Default("CURRENT_TIMESTAMP");
            Map(post => post.Visualizacoes);
            Map(post => post.Likes);
        }
    }



}